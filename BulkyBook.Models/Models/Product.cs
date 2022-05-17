using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBook.Models.Models;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public string ISBN { get; set; } = string.Empty;
    [Required]
    public string Author { get; set; } = string.Empty;
    [Required]
    [Range(1, 10000)]
    public double ListPrice { get; set; }
    [Required]
    [Range(1, 10000)]
    public double Price { get; set; }
    [Required]
    [Range(1, 10000)]
    public double PriceFor5 { get; set; }
    [Required]
    [Range(1, 10000)]
    public double PriceFor10 { get; set; }
    [ValidateNever]
    public string ImageUrl { get; set; } = string.Empty;
    
    [Required]
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    [ValidateNever]
    public Category Category { get; set; }
    [Required]
    public int CoverTypeId { get; set; }
    [ForeignKey("CoverTypeId")]
    [ValidateNever]
    public CoverType CoverType { get; set; }
}
