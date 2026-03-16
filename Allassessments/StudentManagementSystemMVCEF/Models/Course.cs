namespace StudentManagementSystemMVCEF.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;

        public double Duration { get; set; }

        public int DepartmentId { get; set; }

        public Department? Department { get; set; }
    }
}
