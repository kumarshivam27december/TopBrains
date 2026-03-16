using StudentManagementRepoPattern.Models;
using StudentManagementRepoPattern.Repositories;

namespace StudentManagementRepoPattern.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Student> Students { get; }
    IGenericRepository<Department> Departments { get; }
    IGenericRepository<Course> Courses { get; }

    int Save();
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
}

