using BulkyBook.DataAccess.Data;
using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Web.Controllers;

public class CategoryController : Controller
{
    private readonly DataContext _context;

    public CategoryController(DataContext context)
    {
        _context = context;
    }

    // GET: Category
    public async Task<IActionResult> Index()
    {
        return View(await _context.Categories.ToListAsync());
    }

    //GET: Category/Create
    public IActionResult Create()
    {
        return View();
    }

    //POST: Category/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("CustomError", "The Display Order cannot exactly match the Name");
        }
        if (ModelState.IsValid)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            TempData["success"] = "Category Created Successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    // GET: Category/Edit/1
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null & id == 0)
            return NotFound();

        var category = await _context.Categories.FindAsync(id);

        if (category is null)
            return NotFound();

        return View(category);
    }

    //POST: Category/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("CustomError", "The Display Order cannot exactly match the Name");
        }
        if (ModelState.IsValid)
        {
            try
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                TempData["success"] = "Category Updated Successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    //GET: Category/Remove/1
    public async Task<IActionResult> Remove(int? id)
    {
        if (id is null & id == 0)
            return NotFound();

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
            return NotFound();

        return View(category);
    }

    //POST
    [HttpPost, ActionName("Remove")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemovePOST(int? id)
    {

        var category = await _context.Categories.FindAsync(id);
        if (category is null)
            return NotFound();

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        TempData["success"] = "Category Removed Successfully!";
        return RedirectToAction(nameof(Index));
    }

    private bool CategoryExists(int id)
    {
        return _context.Categories.Any(c => c.Id == id);
    }
}
