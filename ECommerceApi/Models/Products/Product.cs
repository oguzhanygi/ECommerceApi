using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models.Categories;
using ECommerceApi.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Models.Products;

public class Product : ISoftDeletable, IHasTimestamps
{
    public required Guid Id { get; init; }

    [MaxLength(128)] [MinLength(2)] public required string Name { get; set; }

    [Range(0.0, double.MaxValue, ErrorMessage = "Price must be > 0.")]
    [Precision(18, 2)] // Stores up to 999,999,999,999,999.99
    public required decimal Price { get; set; }

    [MaxLength(1024)] public string? Description { get; set; }

    [MaxLength(128)] [MinLength(2)] public required string Brand { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Stock must be >= 0.")]
    public int StockQuantity { get; set; }

    public required Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}