using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystemMVCEF.Context;
using StudentManagementSystemMVCEF.Models;

namespace StudentManagementSystemMVCEF.Controllers
{
    public class CourseController : Controller
    {
        private readonly StudentDBContext dbContext;

        public CourseController(StudentDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var courses = dbContext.Courses
                .Include(c => c.Department)
                .ToList();

            return View(courses);
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
        public IActionResult Create(Course course)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(
                    dbContext.Departments.AsNoTracking().ToList(),
                    "DepartmentId",
                    "DepartmentName",
                    course.DepartmentId
                );
                return View(course);
            }

            var departmentExists = dbContext.Departments.Any(d => d.DepartmentId == course.DepartmentId);
            if (!departmentExists) return BadRequest("Invalid DepartmentId");

            dbContext.Courses.Add(course);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

