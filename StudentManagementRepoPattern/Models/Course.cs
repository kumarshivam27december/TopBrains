using System.ComponentModel.DataAnnotations;

namespace StudentManagementRepoPattern.Models;

public class Course
{
    public int CourseId { get; set; }

    [Required]
    [StringLength(150)]
    public string CourseName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Duration { get; set; } = string.Empty;

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    public ICollection<Student> Students { get; set; } = new List<Student>();
}

