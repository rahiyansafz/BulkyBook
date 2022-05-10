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

    public async void Add(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public async Task<T> Find(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        T? t = await query.FirstOrDefaultAsync();
        return t!;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        IQueryable<T> query = dbSet;
        return await query.ToListAsync();
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
