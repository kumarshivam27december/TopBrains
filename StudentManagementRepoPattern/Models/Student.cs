using System.ComponentModel.DataAnnotations;

namespace StudentManagementRepoPattern.Models;

public class Student
{
    public int StudentId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    [Range(18, 60)]
    public int Age { get; set; }

    [Required]
    [StringLength(20)]
    public string Gender { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime AdmissionDate { get; set; } = DateTime.Today;

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    public int CourseId { get; set; }
    public Course? Course { get; set; }
}

