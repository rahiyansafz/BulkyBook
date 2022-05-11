using BulkyBook.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBook.Models.ViewModels;

public class ProductViewModel
{
    public Product Product { get; set; }
    public IEnumerable<SelectListItem> Categories { get; set; }
    public IEnumerable<SelectListItem> CoverTypes { get; set; }
}
