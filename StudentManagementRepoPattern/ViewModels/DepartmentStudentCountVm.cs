namespace StudentManagementRepoPattern.ViewModels;

public class DepartmentStudentCountVm
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public int TotalStudents { get; set; }
}

