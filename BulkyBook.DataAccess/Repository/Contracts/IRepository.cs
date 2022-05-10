using System.Linq.Expressions;


namespace BulkyBook.DataAccess.Repository.Contracts;

public interface IRepository<T> where T : class
{
    T Find(Expression<Func<T, bool>> filter);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entity);
}
