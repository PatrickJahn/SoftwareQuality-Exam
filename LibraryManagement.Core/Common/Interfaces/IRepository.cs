namespace LibraryManagement.Core.Common.Interfaces;


public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T Get(Guid id);
    void Add(T entity);
    void Edit(T entity);
    void Remove(Guid id);
}