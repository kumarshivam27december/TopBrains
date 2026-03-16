using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StudentManagementSystem.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department Name is required.")]
        public string DepartmentName { get; set; }

        public string Description { get; set; }

        // Navigation properties
        [ValidateNever]
        public virtual ICollection<Course> Courses { get; set; }
        [ValidateNever]
        public virtual ICollection<Student> Students { get; set; }
    }
}
