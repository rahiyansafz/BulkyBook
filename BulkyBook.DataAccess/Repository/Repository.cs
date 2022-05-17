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
        //_context.Products.Include(p => p.Category).Include(p => p.CoverType);
        this.dbSet = _context.Set<T>();
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public T Find(Expression<Func<T, bool>> filter, string? includeProps = null)
    {
        IQueryable<T> query = dbSet;

        query = query.Where(filter);
        if (includeProps is not null)
        {
            foreach (var item in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item);
            }
        }

        T? t = query.FirstOrDefault();
        return t!;
    }

    public IEnumerable<T> GetAll(string? includeProps = null)
    {
        IQueryable<T> query = dbSet;
        if (includeProps is not null)
        {
            foreach (var item in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item);
            }
        }
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
