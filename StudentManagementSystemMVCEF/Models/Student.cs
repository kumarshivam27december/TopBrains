using System.ComponentModel.DataAnnotations;
namespace StudentManagementSystemMVCEF.Models
{

    public class Student
    {
 
        public int StudentId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60.")]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
    }
}
