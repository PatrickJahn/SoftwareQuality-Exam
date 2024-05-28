using System.Linq.Expressions;

namespace LibraryManagement.Core.Common.Interfaces;


public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T Get(Guid id);
    Task Add(T entity);
    void Edit(T entity);
    void Remove(Guid id);
    T? Find(Expression<Func<T, bool>> predicate);
    IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate);

}