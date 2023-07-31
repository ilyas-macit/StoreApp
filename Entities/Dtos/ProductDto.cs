using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos;

public record ProductDto
{
    public int Id { get; init; }
    [Required(ErrorMessage = "Product Name is required.")]
    public string ProductName { get; init; }
    [Required(ErrorMessage = "Product Name is required.")]
    public decimal Price { get; init; }
    
    public string? Summary { get; init; }
    
    public string? ImageUrl { get; set; }
    public int? CategoryId { get; init; }
     
}