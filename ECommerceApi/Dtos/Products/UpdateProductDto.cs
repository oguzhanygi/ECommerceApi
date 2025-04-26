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
    
    [Range(0.0, double.MaxValue, ErrorMessage = "Sale price must be >= 0.")]
    public decimal? SalePrice { get; set; }

    [MaxLength(128, ErrorMessage = "Brand cannot exceed 128 characters")]
    [MinLength(2, ErrorMessage = "Brand must be at least 2 characters")]
    public string? Brand { get; set; }
    
    [MaxLength(64, ErrorMessage = "SKU cannot exceed 64 characters")]
    public string? SKU { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock must be >= 0.")]
    public int? StockQuantity { get; set; }
    
    [Range(0.0, double.MaxValue, ErrorMessage = "Weight must be >= 0.")]
    public decimal? Weight { get; set; }
    
    [Range(0.0, double.MaxValue, ErrorMessage = "Length must be >= 0.")]
    public decimal? Length { get; set; }
    
    [Range(0.0, double.MaxValue, ErrorMessage = "Width must be >= 0.")]
    public decimal? Width { get; set; }
    
    [Range(0.0, double.MaxValue, ErrorMessage = "Height must be >= 0.")]
    public decimal? Height { get; set; }
    
    [MaxLength(32, ErrorMessage = "Weight unit cannot exceed 32 characters")]
    public string? WeightUnit { get; set; }
    
    [MaxLength(32, ErrorMessage = "Dimension unit cannot exceed 32 characters")]
    public string? DimensionUnit { get; set; }
    
    public bool? IsAvailable { get; set; }
    
    public bool? IsFeatured { get; set; }
    
    public DateTime? ReleaseDate { get; set; }
    
    public ICollection<string>? ImageUrls { get; set; }
    
    public ICollection<string>? Tags { get; set; }

    public Guid? CategoryId { get; set; }
}