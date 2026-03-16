using Microsoft.AspNetCore.Mvc;
using StudentManagementRepoPattern.Models;
using StudentManagementRepoPattern.UnitOfWorks;
using StudentManagementRepoPattern.ViewModels;
using System.Diagnostics;

namespace StudentManagementRepoPattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _uow;

        public HomeController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            var vm = new DashboardVm
            {
                TotalStudents = _uow.Students.Query().Count(),
                TotalDepartments = _uow.Departments.Query().Count(),
                TotalCourses = _uow.Courses.Query().Count()
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
