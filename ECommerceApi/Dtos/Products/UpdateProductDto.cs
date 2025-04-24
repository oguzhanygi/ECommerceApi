using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.DTOs.Products;

public class UpdateProductDto
{
    [MaxLength(128, ErrorMessage = "Name cannot exceed 128 characters")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
    public string? Name { get; set; }

    [MaxLength(1024, ErrorMessage = "Description cannot exceed 1024 characters")]
    public string? Description { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Price must be > 0.")]
    public decimal? Price { get; set; }

    [MaxLength(128, ErrorMessage = "Brand cannot exceed 128 characters")]
    [MinLength(2, ErrorMessage = "Brand must be at least 2 characters")]
    public string? Brand { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock must be >= 0.")]
    public int? StockQuantity { get; set; }

    public Guid? CategoryId { get; set; }
}