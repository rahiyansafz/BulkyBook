namespace BulkyBook.DataAccess.Repository.Contracts;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    void Save();
}
