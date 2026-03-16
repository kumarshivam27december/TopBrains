using Microsoft.EntityFrameworkCore;
using StudentManagementRepoPattern.Data;
using System.Linq.Expressions;

namespace StudentManagementRepoPattern.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly StudentDbContext Context;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(StudentDbContext context)
    {
        Context = context;
        DbSet = Context.Set<T>();
    }

    public IQueryable<T> Query()
        => DbSet;

    public IEnumerable<T> GetAll()
        => DbSet.AsNoTracking().ToList();

    public T? GetById(object id)
        => DbSet.Find(id);

    public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        => DbSet.AsNoTracking().Where(predicate).ToList();

    public void Insert(T entity)
        => DbSet.Add(entity);

    public void Update(T entity)
        => DbSet.Update(entity);

    public void Delete(object id)
    {
        var entity = DbSet.Find(id);
        if (entity is not null)
        {
            Delete(entity);
        }
    }

    public void Delete(T entity)
        => DbSet.Remove(entity);
}

