using BulkyBook.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Web.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; } = null!;
}
