using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Models;

public class Product : IEntity, ISoftDeletable, IHasTimestamps
{
    [MaxLength(128)] 
    [MinLength(2)]
    public required string Name { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Price must be > 0.")]
    [Precision(18, 2)] // Stores up to 999,999,999,999,999.99
    public required decimal Price { get; set; }
    
    [Range(0.0, double.MaxValue, ErrorMessage = "Sale price must be >= 0.")]
    [Precision(18, 2)]
    public decimal? SalePrice { get; set; }

    [MaxLength(1024)] 
    public string? Description { get; set; }

    [MaxLength(128)] 
    [MinLength(2)] 
    public required string Brand { get; set; }
    
    [MaxLength(64)]
    public string? SKU { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock must be >= 0.")]
    public int StockQuantity { get; set; }
    
    [Range(0.0, double.MaxValue, ErrorMessage = "Weight must be >= 0.")]
    [Precision(10, 2)]
    public decimal? Weight { get; set; }
    
    [Range(0.0, double.MaxValue, ErrorMessage = "Length must be >= 0.")]
    [Precision(10, 2)]
    public decimal? Length { get; set; }
    
    [Range(0.0, double.MaxValue, ErrorMessage = "Width must be >= 0.")]
    [Precision(10, 2)]
    public decimal? Width { get; set; }
    
    [Range(0.0, double.MaxValue, ErrorMessage = "Height must be >= 0.")]
    [Precision(10, 2)]
    public decimal? Height { get; set; }
    
    [MaxLength(32)]
    public string? WeightUnit { get; set; }
    
    [MaxLength(32)]
    public string? DimensionUnit { get; set; }
    
    public bool IsAvailable { get; set; } = true;
    
    public bool IsFeatured { get; set; } = false;
    
    public DateTime? ReleaseDate { get; set; }
    
    public ICollection<string> ImageUrls { get; set; } = new List<string>();
    
    public ICollection<string> Tags { get; set; } = new List<string>();
    
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    
    public double? AverageRating { get; set; }

    public required Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public required Guid Id { get; init; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}