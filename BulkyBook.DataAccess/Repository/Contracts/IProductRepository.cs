using BulkyBook.Models.Models;

namespace BulkyBook.DataAccess.Repository.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
    }
}
