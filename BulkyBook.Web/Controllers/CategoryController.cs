using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.Contracts;
using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Web.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _context;

    public CategoryController(ICategoryRepository context)
    {
        _context = context;
    }

    // GET: Category
    public IActionResult Index()
    {
        IEnumerable<Category> categories = _context.GetAll();
        return View(categories);
    }

    //GET: Category/Create
    public IActionResult Create()
    {
        return View();
    }

    //POST: Category/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("CustomError", "The Display Order cannot exactly match the Name");
        }
        if (ModelState.IsValid)
        {
            _context.Add(category);
            _context.Save();
            TempData["success"] = "Category Created Successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    // GET: Category/Edit/1
    public IActionResult Edit(int? id)
    {
        if (id is null & id == 0)
            return NotFound();

        var category = _context.Find(x => x.Id == id);

        if (category is null)
            return NotFound();

        return View(category);
    }

    //POST: Category/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("CustomError", "The Display Order cannot exactly match the Name");
        }
        if (ModelState.IsValid)
        {

            _context.Update(category);
            _context.Save();
            TempData["success"] = "Category Updated Successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    //GET: Category/Remove/1
    public IActionResult Remove(int? id)
    {
        if (id is null & id == 0)
            return NotFound();

        var category = _context.Find(c => c.Id == id);

        if (category is null)
            return NotFound();

        return View(category);
    }

    //POST
    [HttpPost, ActionName("Remove")]
    [ValidateAntiForgeryToken]
    public IActionResult RemovePOST(int? id)
    {

        var category = _context.Find(c => c.Id == id);
        if (category is null)
            return NotFound();

        _context.Remove(category);
        _context.Save();
        TempData["success"] = "Category Removed Successfully!";
        return RedirectToAction(nameof(Index));
    }
}
