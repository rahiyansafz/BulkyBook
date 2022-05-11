using BulkyBook.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<CoverType> CoverTypes { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
}
