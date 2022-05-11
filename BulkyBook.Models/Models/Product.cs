using System.ComponentModel.DataAnnotations;

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
}
