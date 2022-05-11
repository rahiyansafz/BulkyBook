namespace BulkyBook.DataAccess.Repository.Contracts;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    ICoverTypeRepository CoverType { get; }
    IProductRepository Product { get; }
    void Save();
}
