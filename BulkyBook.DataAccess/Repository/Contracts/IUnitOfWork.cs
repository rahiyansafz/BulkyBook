namespace BulkyBook.DataAccess.Repository.Contracts;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    ICoverTypeRepository CoverType { get; }
    void Save();
}
