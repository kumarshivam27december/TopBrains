using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementRepoPattern.Models;
using StudentManagementRepoPattern.UnitOfWorks;
using StudentManagementRepoPattern.ViewModels;

namespace StudentManagementRepoPattern.Controllers;

public class StudentController : Controller
{
    private readonly IUnitOfWork _uow;

    public StudentController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IActionResult> Index()
    {
        var students = await _uow.Students.Query()
            .AsNoTracking()
            .Include(s => s.Department)
            .Include(s => s.Course)
            .OrderBy(s => s.Name)
            .ToListAsync();

        return View(students);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var student = await _uow.Students.Query()
            .AsNoTracking()
            .Include(s => s.Department)
            .Include(s => s.Course)
            .FirstOrDefaultAsync(s => s.StudentId == id.Value);

        return student is null ? NotFound() : View(student);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateDropdownsAsync();
        return View(new Student { AdmissionDate = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync(student.DepartmentId, student.CourseId);
            return View(student);
        }

        _uow.Students.Insert(student);
        await _uow.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var student = _uow.Students.GetById(id.Value);
        if (student is null) return NotFound();

        await PopulateDropdownsAsync(student.DepartmentId, student.CourseId);
        return View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if (id != student.StudentId) return NotFound();

        if (!ModelState.IsValid)
        {
            await PopulateDropdownsAsync(student.DepartmentId, student.CourseId);
            return View(student);
        }

        _uow.Students.Update(student);
        await _uow.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var student = await _uow.Students.Query()
            .AsNoTracking()
            .Include(s => s.Department)
            .Include(s => s.Course)
            .FirstOrDefaultAsync(s => s.StudentId == id.Value);

        return student is null ? NotFound() : View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        _uow.Students.Delete(id);
        await _uow.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    // -------- Advanced LINQ Endpoints --------

    // /Student/Search?name=John
    public async Task<IActionResult> Search(string? name)
    {
        name ??= string.Empty;

        var students = await _uow.Students.Query()
            .AsNoTracking()
            .Include(s => s.Department)
            .Include(s => s.Course)
            .Where(s => s.Name.Contains(name))
            .OrderBy(s => s.Name)
            .ToListAsync();

        ViewBag.SearchName = name;
        return View("Index", students);
    }

    // /Student/ByDepartment/2
    public async Task<IActionResult> ByDepartment(int id)
    {
        var students = await _uow.Students.Query()
            .AsNoTracking()
            .Include(s => s.Department)
            .Include(s => s.Course)
            .Where(s => s.DepartmentId == id)
            .OrderBy(s => s.Name)
            .ToListAsync();

        ViewBag.FilterTitle = $"Department ID: {id}";
        return View("Index", students);
    }

    // /Student/ByCourse/5
    public async Task<IActionResult> ByCourse(int id)
    {
        var students = await _uow.Students.Query()
            .AsNoTracking()
            .Include(s => s.Department)
            .Include(s => s.Course)
            .Where(s => s.CourseId == id)
            .OrderBy(s => s.Name)
            .ToListAsync();

        ViewBag.FilterTitle = $"Course ID: {id}";
        return View("Index", students);
    }

    public async Task<IActionResult> OlderThan25()
    {
        var students = await _uow.Students.Query()
            .AsNoTracking()
            .Include(s => s.Department)
            .Include(s => s.Course)
            .Where(s => s.Age > 25)
            .OrderBy(s => s.Name)
            .ToListAsync();

        ViewBag.FilterTitle = "Students older than 25";
        return View("Index", students);
    }

    public async Task<IActionResult> AdmittedAfter2024()
    {
        var cutoff = DateTime.Now.AddYears(-1);

        var students = await _uow.Students.Query()
            .AsNoTracking()
            .Include(s => s.Department)
            .Include(s => s.Course)
            .Where(s => s.AdmissionDate > cutoff)
            .OrderByDescending(s => s.AdmissionDate)
            .ToListAsync();

        ViewBag.FilterTitle = $"Admitted after {cutoff:yyyy-MM-dd}";
        return View("Index", students);
    }

    public async Task<IActionResult> OrderByName()
    {
        var students = await _uow.Students.Query()
            .AsNoTracking()
            .Include(s => s.Department)
            .Include(s => s.Course)
            .OrderBy(s => s.Name)
            .ToListAsync();

        ViewBag.FilterTitle = "Ordered by Name";
        return View("Index", students);
    }

    public async Task<IActionResult> Top5RecentAdmissions()
    {
        var students = await _uow.Students.Query()
            .AsNoTracking()
            .Include(s => s.Department)
            .Include(s => s.Course)
            .OrderByDescending(s => s.AdmissionDate)
            .Take(5)
            .ToListAsync();

        ViewBag.FilterTitle = "Top 5 Recent Admissions";
        return View("Index", students);
    }

    public async Task<IActionResult> TotalStudentsPerDepartment()
    {
        var report = await _uow.Students.Query()
            .AsNoTracking()
            .GroupBy(s => s.DepartmentId)
            .Select(g => new { DepartmentId = g.Key, TotalStudents = g.Count() })
            .Join(
                _uow.Departments.Query().AsNoTracking(),
                s => s.DepartmentId,
                d => d.DepartmentId,
                (s, d) => new DepartmentStudentCountVm
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    TotalStudents = s.TotalStudents
                })
            .OrderBy(r => r.DepartmentName)
            .ToListAsync();

        return View(report);
    }

    // Bonus: export current students list to JSON
    public async Task<IActionResult> ExportJson()
    {
        var students = await _uow.Students.Query()
            .AsNoTracking()
            .Include(s => s.Department)
            .Include(s => s.Course)
            .OrderBy(s => s.StudentId)
            .ToListAsync();

        return Json(students.Select(s => new
        {
            s.StudentId,
            s.Name,
            s.Email,
            s.Age,
            s.Gender,
            s.AdmissionDate,
            Department = s.Department?.DepartmentName,
            Course = s.Course?.CourseName
        }));
    }

    private async Task PopulateDropdownsAsync(int? selectedDepartmentId = null, int? selectedCourseId = null)
    {
        var departments = await _uow.Departments.Query()
            .AsNoTracking()
            .OrderBy(d => d.DepartmentName)
            .ToListAsync();

        var courses = await _uow.Courses.Query()
            .AsNoTracking()
            .OrderBy(c => c.CourseName)
            .ToListAsync();

        ViewData["DepartmentId"] = new SelectList(departments, nameof(Department.DepartmentId), nameof(Department.DepartmentName), selectedDepartmentId);
        ViewData["CourseId"] = new SelectList(courses, nameof(Course.CourseId), nameof(Course.CourseName), selectedCourseId);
    }
}

