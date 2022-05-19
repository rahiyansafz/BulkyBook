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
            productViewModel.Product = _unitOfWork.Product.Find(x => x.Id == id);
            return View(productViewModel);
        }
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

                if (upsertProduct.Product.ImageUrl is not null)
                {
                    var existingImagePath = Path.Combine(wwwRootPath, upsertProduct.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(existingImagePath))
                    {
                        System.IO.File.Delete(existingImagePath);
                    }
                }

                using (var stream = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                upsertProduct.Product.ImageUrl = @"\images\products\" + fileName + ext;
            }

            if (upsertProduct.Product.Id == 0)
            {
                _unitOfWork.Product.Add(upsertProduct.Product);
            }
            else
            {
                _unitOfWork.Product.Update(upsertProduct.Product);
            }
            _unitOfWork.Save();
            TempData["success"] = "Product Added Successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(upsertProduct);
    }

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _unitOfWork.Product.GetAll(includeProps: "Category,CoverType");
        return Json(new { data = products });
    }

    [HttpDelete]
    public IActionResult Remove(int? id)
    {

        var product = _unitOfWork.Product.Find(c => c.Id == id);
        if (product is null)
            return Json(new { success = false, message = "Error while deleting! Tell your developoer to check server!" });

        var existingImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
        if (System.IO.File.Exists(existingImagePath))
            System.IO.File.Delete(existingImagePath);

        _unitOfWork.Product.Remove(product);
        _unitOfWork.Save();
        return Json(new { success = false, message = "Successfully Deleted!!!" });
    }
    #endregion
}
