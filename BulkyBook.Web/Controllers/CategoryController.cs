using BulkyBook.Web.Data;
using BulkyBook.Web.Models;
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
        IEnumerable<Category> objCategoryList = _dataContext.Categories;
        return View(objCategoryList);
    }

    //GET
    public IActionResult Create()
    {
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if(obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("CustomError", "The Display Order cannot exactly match the Name");
        }
        if (ModelState.IsValid)
        {
            _dataContext.Categories.Add(obj);
            _dataContext.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(obj);
    }
}
