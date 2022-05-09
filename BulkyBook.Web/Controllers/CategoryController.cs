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
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("CustomError", "The Display Order cannot exactly match the Name");
        }
        if (ModelState.IsValid)
        {
            _dataContext.Categories.Add(obj);
            _dataContext.SaveChanges();
            TempData["success"] = "Category Created Successfully!";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //GET
    public IActionResult Edit(int? id)
    {
        if (id is null & id is 0)
            return NotFound();

        var category = _dataContext.Categories.FirstOrDefault(c => c.Id == id);

        if (category is null)
            return NotFound();

        return View(category);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("CustomError", "The Display Order cannot exactly match the Name");
        }
        if (ModelState.IsValid)
        {
            _dataContext.Categories.Update(obj);
            _dataContext.SaveChanges();
            TempData["success"] = "Category Updated Successfully!";
            return RedirectToAction("Index");
        }
        return View(obj);
    }

    //GET
    public IActionResult Remove(int? id)
    {
        if (id is null & id is 0)
            return NotFound();

        var category = _dataContext.Categories.FirstOrDefault(c => c.Id == id);

        if (category is null)
            return NotFound();

        return View(category);
    }

    //POST
    [HttpPost, ActionName("Remove")]
    [ValidateAntiForgeryToken]
    public IActionResult RemovePOST(int? id)
    {

        var category = _dataContext.Categories.FirstOrDefault(c => c.Id == id);
        if (category is null)
            return NotFound();

        _dataContext.Categories.Remove(category);
        _dataContext.SaveChanges();
        TempData["success"] = "Category Removed Successfully!";
        return RedirectToAction("Index");
    }
}
