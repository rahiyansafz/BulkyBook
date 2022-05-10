using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.Contracts;
using BulkyBook.Models.Models;

namespace BulkyBook.DataAccess.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly DataContext _context;

    public CategoryRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public void Update(Category category)
    {
        _context.Categories.Update(category);
    }
}
