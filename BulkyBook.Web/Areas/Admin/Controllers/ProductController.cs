using BulkyBook.DataAccess.Repository.Contracts;
using BulkyBook.Models.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace BulkyBook.Web.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        return View();
    }

    // GET: Product/Edit/1
    public IActionResult Upsert(int? id)
    {
        ProductViewModel productViewModel = new()
        {
            Product = new(),
            Categories = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
            CoverTypes = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            }),
        };

        if (id is null || id == 0)
        {
            //ViewBag.Categories = Categories;
            //ViewData["CoverTypes"] = CoverTypes;
            return View(productViewModel);
        }
        else
        {

        }

        return View(productViewModel);
    }

    //POST: Product/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductViewModel upsertProduct, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if (file is not null)
            {
                string fileName = Guid.NewGuid().ToString();
                var upload = Path.Combine(wwwRootPath, @"images\products");
                var ext = Path.GetExtension(file.FileName);

                using (var stream = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                upsertProduct.Product.ImageUrl = @"\images\products\" + fileName + ext;
            }

            _unitOfWork.Product.Add(upsertProduct.Product);
            _unitOfWork.Save();
            TempData["success"] = "Product Added Successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(upsertProduct);
    }

    ////GET: Product/Remove/1
    //public IActionResult Remove(int? id)
    //{
    //    if (id is null || id == 0)
    //        return NotFound();

    //    var product = _unitOfWork.Product.Find(c => c.Id == id);

    //    if (product is null)
    //        return NotFound();

    //    return View(product);
    //}

    ////POST
    //[HttpPost, ActionName("Remove")]
    //[ValidateAntiForgeryToken]
    //public IActionResult RemovePOST(int? id)
    //{

    //    var product = _unitOfWork.Product.Find(c => c.Id == id);
    //    if (product is null)
    //        return NotFound();

    //    _unitOfWork.Product.Remove(product);
    //    _unitOfWork.Save();
    //    TempData["success"] = "Product Removed Successfully!";
    //    return RedirectToAction(nameof(Index));
    //}

    #region API CALLS
    [HttpGet]
    public ActionResult GetAll()
    {
        var products = _unitOfWork.Product.GetAll();
        return Json(new { data = products });
    }
    #endregion
}
