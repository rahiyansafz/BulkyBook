using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBook.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DataContext _context;
    internal DbSet<T> dbSet;

    public Repository(DataContext context)
    {
        _context = context;
        this.dbSet = _context.Set<T>();
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public T Find(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        T? t = query.FirstOrDefault();
        return t!;
    }

    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
        return query.ToList();
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);
    }
}
