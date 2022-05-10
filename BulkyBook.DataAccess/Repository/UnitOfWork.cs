using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.Contracts;

namespace BulkyBook.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    public UnitOfWork(DataContext context)
    {
        _context = context;
        Category = new CategoryRepository(_context);
    }

    public ICategoryRepository Category { get; private set; }

    public void Save()
    {
        _context.SaveChanges();
    }
}
