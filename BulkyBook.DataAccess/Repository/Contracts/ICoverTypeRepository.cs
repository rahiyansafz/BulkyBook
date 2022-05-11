using BulkyBook.Models.Models;

namespace BulkyBook.DataAccess.Repository.Contracts;

public interface ICoverTypeRepository : IRepository<CoverType>
{
    void Update(CoverType category);
}
