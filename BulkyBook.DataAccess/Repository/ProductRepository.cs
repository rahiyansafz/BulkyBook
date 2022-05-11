using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.Contracts;
using BulkyBook.Models.Models;

namespace BulkyBook.DataAccess.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly DataContext _context;

    public ProductRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public void Update(Product product)
    {
        var findProduct = _context.Products.FirstOrDefault(x => x.Id == product.Id);
        if (findProduct is not null)
        {
            findProduct.Title = product.Title;
            findProduct.ISBN = product.ISBN;
            findProduct.Price = product.Price;
            findProduct.ListPrice = product.ListPrice;
            findProduct.PriceFor5 = product.PriceFor5;
            findProduct.PriceFor10 = product.PriceFor10;
            findProduct.Description = product.Description;
            findProduct.CategoryId = product.CategoryId;
            findProduct.Author = product.Author;
            findProduct.CoverTypeId = product.CoverTypeId;
            if (findProduct.ImageUrl is not null)
                findProduct.ImageUrl = findProduct.ImageUrl.ToString();
        }
        //_context.Products.Update(product);
    }
}
