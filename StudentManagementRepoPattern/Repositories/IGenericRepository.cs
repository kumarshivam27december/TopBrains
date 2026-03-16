using System.Linq.Expressions;

namespace StudentManagementRepoPattern.Repositories;

public interface IGenericRepository<T> where T : class
{
    IQueryable<T> Query();
    IEnumerable<T> GetAll();
    T? GetById(object id);
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    void Insert(T entity);
    void Update(T entity);
    void Delete(object id);
    void Delete(T entity);
}

