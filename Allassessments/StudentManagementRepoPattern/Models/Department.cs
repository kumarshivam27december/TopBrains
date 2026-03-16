using System.ComponentModel.DataAnnotations;

namespace StudentManagementRepoPattern.Models;

public class Department
{
    public int DepartmentId { get; set; }

    [Required]
    [StringLength(100)]
    public string DepartmentName { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Location { get; set; }

    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}

