using Microsoft.EntityFrameworkCore;
using StudentManagementRepoPattern.Models;

namespace StudentManagementRepoPattern.Data;

public class StudentDbContext : DbContext
{
    public StudentDbContext(DbContextOptions<StudentDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Course> Courses => Set<Course>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Department)
            .WithMany(d => d.Students)
            .HasForeignKey(s => s.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Student>()
            .HasOne(s => s.Course)
            .WithMany(c => c.Students)
            .HasForeignKey(s => s.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.Department)
            .WithMany(d => d.Courses)
            .HasForeignKey(c => c.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed Departments
        modelBuilder.Entity<Department>().HasData(
            new Department { DepartmentId = 1, DepartmentName = "Computer Science", Location = "Building A" },
            new Department { DepartmentId = 2, DepartmentName = "Information Technology", Location = "Building B" },
            new Department { DepartmentId = 3, DepartmentName = "Business Administration", Location = "Building C" }
        );

        // Seed Courses
        modelBuilder.Entity<Course>().HasData(
            new Course { CourseId = 1, CourseName = ".NET Full Stack Development", Duration = "6 Months", DepartmentId = 1 },
            new Course { CourseId = 2, CourseName = "Angular Development", Duration = "4 Months", DepartmentId = 1 },
            new Course { CourseId = 3, CourseName = "Cloud Computing", Duration = "6 Months", DepartmentId = 2 },
            new Course { CourseId = 4, CourseName = "Cyber Security", Duration = "5 Months", DepartmentId = 2 },
            new Course { CourseId = 5, CourseName = "Financial Management", Duration = "3 Months", DepartmentId = 3 }
        );

        // Seed Students
        modelBuilder.Entity<Student>().HasData(
            new Student
            {
                StudentId = 1,
                Name = "Ravi Kumar",
                Email = "ravi@gmail.com",
                Age = 22,
                Gender = "Male",
                AdmissionDate = new DateTime(2024, 07, 10),
                DepartmentId = 1,
                CourseId = 1
            },
            new Student
            {
                StudentId = 2,
                Name = "Anjali Sharma",
                Email = "anjali@gmail.com",
                Age = 23,
                Gender = "Female",
                AdmissionDate = new DateTime(2024, 08, 05),
                DepartmentId = 2,
                CourseId = 3
            },
            new Student
            {
                StudentId = 3,
                Name = "Suresh Reddy",
                Email = "suresh@gmail.com",
                Age = 24,
                Gender = "Male",
                AdmissionDate = new DateTime(2024, 06, 18),
                DepartmentId = 1,
                CourseId = 2
            },
            new Student
            {
                StudentId = 4,
                Name = "Priya Nair",
                Email = "priya@gmail.com",
                Age = 21,
                Gender = "Female",
                AdmissionDate = new DateTime(2024, 09, 01),
                DepartmentId = 3,
                CourseId = 5
            }
        );
    }
}

