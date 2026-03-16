using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Student Name is required.")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Course is required.")]
        public int CourseId { get; set; }

        [ForeignKey("DepartmentId")]
        [ValidateNever]
        public virtual Department Department { get; set; }

        [ForeignKey("CourseId")]
        [ValidateNever]
        public virtual Course Course { get; set; }
    }
}
