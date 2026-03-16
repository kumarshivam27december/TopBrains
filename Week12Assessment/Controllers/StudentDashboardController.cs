using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.ViewModels;
using System.Security.Claims;

namespace StudentManagementSystem.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var student = await _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .FirstOrDefaultAsync(s => s.Email == email);

            if (student == null)
            {
                return NotFound("Student profile not found.");
            }

            var model = new StudentProfileViewModel
            {
                StudentName = student.StudentName,
                Email = student.Email,
                DepartmentName = student.Department?.DepartmentName,
                CourseName = student.Course?.CourseName,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(StudentProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == email);

                if (student != null)
                {
                    student.PhoneNumber = model.PhoneNumber;
                    student.Address = model.Address;
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Profile updated successfully!";
                }
            }
            // Need to reload the rest of the read-only properties
            var studentToReload = await _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .FirstOrDefaultAsync(s => s.Email == User.FindFirstValue(ClaimTypes.Email));
                
            model.StudentName = studentToReload.StudentName;
            model.Email = studentToReload.Email;
            model.DepartmentName = studentToReload.Department?.DepartmentName;
            model.CourseName = studentToReload.Course?.CourseName;

            return View("Index", model);
        }
    }
}
