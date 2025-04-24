using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.DTOs.Products;

public class CreateProductDto
{
    [MaxLength(128, ErrorMessage = "Name cannot exceed 128 characters")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
    public required string Name { get; set; }

    [MaxLength(1024, ErrorMessage = "Description cannot exceed 1024 characters")]
    public string? Description { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Price must be > 0.")]
    public required decimal Price { get; set; }

    [MaxLength(128, ErrorMessage = "Brand cannot exceed 128 characters")]
    [MinLength(2, ErrorMessage = "Brand must be at least 2 characters")]
    public required string Brand { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock must be >= 0.")]
    public int StockQuantity { get; set; }

    public required Guid CategoryId { get; set; }
}