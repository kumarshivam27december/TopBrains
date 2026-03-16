using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StudentManagementSystem.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course Name is required.")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "Fees are required.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Fees { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        // Navigation properties
        [ForeignKey("DepartmentId")]
        [ValidateNever]
        public virtual Department Department { get; set; }
        [ValidateNever]
        public virtual ICollection<Student> Students { get; set; }
    }
}
