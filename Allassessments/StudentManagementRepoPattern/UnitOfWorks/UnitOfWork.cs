using StudentManagementRepoPattern.Data;
using StudentManagementRepoPattern.Models;
using StudentManagementRepoPattern.Repositories;

namespace StudentManagementRepoPattern.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly StudentDbContext _context;

    public UnitOfWork(StudentDbContext context)
    {
        _context = context;
        Students = new GenericRepository<Student>(_context);
        Departments = new GenericRepository<Department>(_context);
        Courses = new GenericRepository<Course>(_context);
    }

    public IGenericRepository<Student> Students { get; }
    public IGenericRepository<Department> Departments { get; }
    public IGenericRepository<Course> Courses { get; }

    public int Save() => _context.SaveChanges();

    public Task<int> SaveAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);

    public void Dispose()
        => _context.Dispose();
}

