using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace Entities.Models;

public class Category
{
    public int Id { get; set; }
    [Required]
    public string CategoryName { get; set; }
    public ICollection<Product>? Products { get; set; }
}