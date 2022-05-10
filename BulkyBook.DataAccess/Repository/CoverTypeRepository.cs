using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.Contracts;
using BulkyBook.Models.Models;

namespace BulkyBook.DataAccess.Repository;

public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
{
    private readonly DataContext _context;

    public CoverTypeRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public void Update(CoverType coverType)
    {
        _context.CoverTypes.Update(coverType);
    }
}
