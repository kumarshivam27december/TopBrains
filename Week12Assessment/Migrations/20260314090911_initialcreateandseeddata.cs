using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Week12Assessment.Migrations
{
    /// <inheritdoc />
    public partial class initialcreateandseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentName", "Description" },
                values: new object[,]
                {
                    { 1, "Computer Science", "CS Department" },
                    { 2, "Information Technology", "IT Department" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FullName", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "admin@institute.com", "Admin Teacher", "password123", "Teacher" },
                    { 2, "john@student.com", "John Doe", "password123", "Student" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CourseName", "DepartmentId", "Duration", "Fees" },
                values: new object[,]
                {
                    { 1, "B.Tech CS", 1, "4 Years", 50000.00m },
                    { 2, "B.Tech IT", 2, "4 Years", 45000.00m }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Address", "CourseId", "DepartmentId", "Email", "PhoneNumber", "StudentName" },
                values: new object[] { 1, "123 Main St", 1, 1, "john@student.com", "1234567890", "John Doe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentId",
                keyValue: 1);
        }
    }
}
