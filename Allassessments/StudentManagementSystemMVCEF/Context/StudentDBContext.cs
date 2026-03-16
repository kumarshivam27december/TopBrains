using Microsoft.EntityFrameworkCore;
using StudentManagementSystemMVCEF.Models;


namespace StudentManagementSystemMVCEF.Context
{
    public class StudentDBContext : DbContext
    {
        public StudentDBContext(DbContextOptions<StudentDBContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Course> Courses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    DepartmentId = 1,
                    DepartmentName = "Computer Science",
                    Location = "Block A"
                },
                new Department
                {
                    DepartmentId = 2,
                    DepartmentName = "Mechanical",
                    Location = "Block B"
                },
                new Department
                {
                    DepartmentId = 3,
                    DepartmentName = "Electrical",
                    Location = "Block C"
                }
            );
        }
    }
}
