using BulkyBook.DataAccess.Repository.Contracts;
using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Web.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> products = _unitOfWork.Product.GetAll();
        return View(products);
    }

    //GET: Product/Create
    public IActionResult Create()
    {
        return View();
    }

    //POST: Product/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Product.Add(product);
            _unitOfWork.Save();
            TempData["success"] = "Product Created Successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: Product/Edit/1
    public IActionResult Edit(int? id)
    {
        if (id is null & id == 0)
            return NotFound();

        var product = _unitOfWork.Product.Find(x => x.Id == id);

        if (product is null)
            return NotFound();

        return View(product);
    }

    //POST: Product/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {

            _unitOfWork.Product.Update(product);
            _unitOfWork.Save();
            TempData["success"] = "Product Updated Successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    //GET: Product/Remove/1
    public IActionResult Remove(int? id)
    {
        if (id is null & id == 0)
            return NotFound();

        var product = _unitOfWork.Product.Find(c => c.Id == id);

        if (product is null)
            return NotFound();

        return View(product);
    }

    //POST
    [HttpPost, ActionName("Remove")]
    [ValidateAntiForgeryToken]
    public IActionResult RemovePOST(int? id)
    {

        var product = _unitOfWork.Product.Find(c => c.Id == id);
        if (product is null)
            return NotFound();

        _unitOfWork.Product.Remove(product);
        _unitOfWork.Save();
        TempData["success"] = "Product Removed Successfully!";
        return RedirectToAction(nameof(Index));
    }
}
