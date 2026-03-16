using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementRepoPattern.Models;
using StudentManagementRepoPattern.UnitOfWorks;

namespace StudentManagementRepoPattern.Controllers;

public class CourseController : Controller
{
    private readonly IUnitOfWork _uow;

    public CourseController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _uow.Courses.Query()
            .AsNoTracking()
            .Include(c => c.Department)
            .OrderBy(c => c.CourseName)
            .ToListAsync();

        return View(courses);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var course = await _uow.Courses.Query()
            .AsNoTracking()
            .Include(c => c.Department)
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.CourseId == id.Value);

        return course is null ? NotFound() : View(course);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateDepartmentsAsync();
        return View(new Course());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Course course)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDepartmentsAsync(course.DepartmentId);
            return View(course);
        }

        _uow.Courses.Insert(course);
        await _uow.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();
        var course = _uow.Courses.GetById(id.Value);
        if (course is null) return NotFound();

        await PopulateDepartmentsAsync(course.DepartmentId);
        return View(course);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Course course)
    {
        if (id != course.CourseId) return NotFound();

        if (!ModelState.IsValid)
        {
            await PopulateDepartmentsAsync(course.DepartmentId);
            return View(course);
        }

        _uow.Courses.Update(course);
        await _uow.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var course = await _uow.Courses.Query()
            .AsNoTracking()
            .Include(c => c.Department)
            .FirstOrDefaultAsync(c => c.CourseId == id.Value);

        return course is null ? NotFound() : View(course);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        _uow.Courses.Delete(id);
        await _uow.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateDepartmentsAsync(int? selectedDepartmentId = null)
    {
        var departments = await _uow.Departments.Query()
            .AsNoTracking()
            .OrderBy(d => d.DepartmentName)
            .ToListAsync();

        ViewData["DepartmentId"] = new SelectList(departments, nameof(Department.DepartmentId), nameof(Department.DepartmentName), selectedDepartmentId);
    }
}

