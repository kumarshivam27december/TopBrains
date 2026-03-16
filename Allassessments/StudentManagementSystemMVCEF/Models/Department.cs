using System.Collections.Generic;

namespace StudentManagementSystemMVCEF.Models
{
    public class Department
    {
        
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public ICollection<Student> Students { get; set; } = new List<Student>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}
