using BulkyBook.DataAccess.Repository.Contracts;
using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Web.Areas.Admin.Controllers;
[Area("Admin")]
public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CoverTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<CoverType> coverTypes = _unitOfWork.CoverType.GetAll();
        return View(coverTypes);
    }

    //GET: CoverType/Create
    public IActionResult Create()
    {
        return View();
    }

    //POST: CoverType/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType coverType)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Add(coverType);
            _unitOfWork.Save();
            TempData["success"] = "CoverType Created Successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(coverType);
    }

    // GET: CoverType/Edit/1
    public IActionResult Edit(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var coverType = _unitOfWork.CoverType.Find(x => x.Id == id);

        if (coverType is null)
            return NotFound();

        return View(coverType);
    }

    //POST: CoverType/Edit/1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType coverType)
    {
        if (ModelState.IsValid)
        {

            _unitOfWork.CoverType.Update(coverType);
            _unitOfWork.Save();
            TempData["success"] = "CoverType Updated Successfully!";
            return RedirectToAction(nameof(Index));
        }
        return View(coverType);
    }

    //GET: CoverType/Remove/1
    public IActionResult Remove(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var coverType = _unitOfWork.CoverType.Find(c => c.Id == id);

        if (coverType is null)
            return NotFound();

        return View(coverType);
    }

    //POST
    [HttpPost, ActionName("Remove")]
    [ValidateAntiForgeryToken]
    public IActionResult RemovePOST(int? id)
    {

        var coverType = _unitOfWork.CoverType.Find(c => c.Id == id);
        if (coverType is null)
            return NotFound();

        _unitOfWork.CoverType.Remove(coverType);
        _unitOfWork.Save();
        TempData["success"] = "CoverType Removed Successfully!";
        return RedirectToAction(nameof(Index));
    }
}
