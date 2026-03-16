using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemMVCEF.Context;
using StudentManagementSystemMVCEF.Models;

namespace StudentManagementSystemMVCEF.Controllers
{
    public class StudentController : Controller
    {

        private readonly StudentDBContext dbContext;

        public StudentController(StudentDBContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public IActionResult Index()
        {
            ViewBag.Departments = new SelectList(
                dbContext.Departments.AsNoTracking().ToList(),
                "DepartmentId",
                "DepartmentName"
            );

            var students = dbContext.Students
                .Include(s => s.Department)
                .ToList();
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(
                dbContext.Departments.AsNoTracking().ToList(),
                "DepartmentId",
                "DepartmentName"
            );
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                var departmentExists = dbContext.Departments.Any(d => d.DepartmentId == student.DepartmentId);
                if (!departmentExists)
                {
                    return BadRequest("Invalid DepartmentId");
                }

                dbContext.Students.Add(student);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = new SelectList(
                dbContext.Departments.AsNoTracking().ToList(),
                "DepartmentId",
                "DepartmentName",
                student.DepartmentId
            );
            return View(student);
        }

        public IActionResult Details(int id)
        {
            if (id <= 0) return BadRequest();

            var student = dbContext.Students
                .Include(s => s.Department)
                .FirstOrDefault(s => s.StudentId == id);

            if (student == null)
                return NotFound();

            return View(student);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id <= 0) return BadRequest();

            var student = dbContext.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null) return NotFound();

            ViewBag.Departments = new SelectList(
                dbContext.Departments.AsNoTracking().ToList(),
                "DepartmentId",
                "DepartmentName",
                student.DepartmentId
            );

            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (student.StudentId <= 0) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(
                    dbContext.Departments.AsNoTracking().ToList(),
                    "DepartmentId",
                    "DepartmentName",
                    student.DepartmentId
                );
                return View(student);
            }

            var existing = dbContext.Students.FirstOrDefault(s => s.StudentId == student.StudentId);
            if (existing == null) return NotFound();

            var departmentExists = dbContext.Departments.Any(d => d.DepartmentId == student.DepartmentId);
            if (!departmentExists) return BadRequest("Invalid DepartmentId");

            existing.Name = student.Name;
            existing.Email = student.Email;
            existing.Password = student.Password;
            existing.Age = student.Age;
            existing.Gender = student.Gender;
            existing.DepartmentId = student.DepartmentId;

            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();

            var student = dbContext.Students
                .Include(s => s.Department)
                .FirstOrDefault(s => s.StudentId == id);

            if (student == null) return NotFound();

            return View(student);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            if (id <= 0) return BadRequest();

            var student = dbContext.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null) return NotFound();

            dbContext.Students.Remove(student);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public JsonResult JsonData()
        {
            var students = dbContext.Students.ToList();
            return Json(students);
        }

        public ContentResult Message()
        {
            return Content("Welcome to Student Management System");
        }

        public IActionResult RedirectToDepartment()
        {
            return RedirectToAction("Index", "Department");
        }

        public ActionResult<Student> GetStudent(int id)
        {
            if (id <= 0) return BadRequest();

            var student = dbContext.Students
                .Include(s => s.Department)
                .FirstOrDefault(s => s.StudentId == id);

            if (student == null) return NotFound();

            return Ok(student);
        }

        public IActionResult Search(string? name, int? departmentId)
        {
            var query = dbContext.Students
                .Include(s => s.Department)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(s => s.Name.Contains(name));
            }

            if (departmentId.HasValue && departmentId.Value > 0)
            {
                query = query.Where(s => s.DepartmentId == departmentId.Value);
            }

            ViewBag.SearchName = name;
            ViewBag.Departments = new SelectList(
                dbContext.Departments.AsNoTracking().ToList(),
                "DepartmentId",
                "DepartmentName",
                departmentId
            );

            return View("Index", query.ToList());
        }

        public IActionResult ServerErrorDemo()
        {
            return StatusCode(500);
        }

    }
}