using System.Linq.Expressions;

namespace BulkyBook.DataAccess.Repository.Contracts;

public interface IRepository<T> where T : class
{
    T Find(Expression<Func<T, bool>> filter, string? includeProps = null);
    IEnumerable<T> GetAll(string? includeProps = null);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entity);
}
