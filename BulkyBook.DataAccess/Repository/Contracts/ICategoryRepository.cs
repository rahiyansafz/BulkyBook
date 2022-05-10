using BulkyBook.Models.Models;

namespace BulkyBook.DataAccess.Repository.Contracts;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category category);
}
