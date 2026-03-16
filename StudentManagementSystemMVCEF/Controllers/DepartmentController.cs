using Microsoft.AspNetCore.Mvc;
using StudentManagementSystemMVCEF.Context;
using StudentManagementSystemMVCEF.Models;

namespace StudentManagementSystemMVCEF.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly StudentDBContext dbContext;

        public DepartmentController(StudentDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var departments = dbContext.Departments.ToList();
            ViewData["Message"] = "Welcome to Student Portal";
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Title = "Create Department";
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (!ModelState.IsValid) return View(department);

            dbContext.Departments.Add(department);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

