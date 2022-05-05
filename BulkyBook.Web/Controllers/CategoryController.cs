using BulkyBook.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Web.Controllers;

public class CategoryController : Controller
{
    private readonly DataContext _dataContext;

    public CategoryController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public IActionResult Index()
    {
        var objCategoryList = _dataContext.Categories.ToList();
        return View();
    }
}
