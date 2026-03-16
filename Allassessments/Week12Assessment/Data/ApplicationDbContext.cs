using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships if needed, though conventions should cover most.
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

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

            // Seed Data
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, FullName = "Admin Teacher", Email = "admin@institute.com", Password = "password123", Role = "Teacher" },
                new User { UserId = 2, FullName = "John Doe", Email = "john@student.com", Password = "password123", Role = "Student" }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "Computer Science", Description = "CS Department" },
                new Department { DepartmentId = 2, DepartmentName = "Information Technology", Description = "IT Department" }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, CourseName = "B.Tech CS", Duration = "4 Years", Fees = 50000.00m, DepartmentId = 1 },
                new Course { CourseId = 2, CourseName = "B.Tech IT", Duration = "4 Years", Fees = 45000.00m, DepartmentId = 2 }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, StudentName = "John Doe", Email = "john@student.com", PhoneNumber = "1234567890", Address = "123 Main St", DepartmentId = 1, CourseId = 1 }
            );
        }
    }
}
