using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Models;

public class Discount : IEntity, ISoftDeletable, IHasTimestamps
{
    [MaxLength(32)]
    [Required]
    public required string Code { get; set; }
    
    [MaxLength(128)]
    public required string Name { get; set; }
    
    [MaxLength(512)]
    public string? Description { get; set; }
    
    public DiscountType Type { get; set; } = DiscountType.Percentage;
    
    [Precision(18, 2)]
    [Range(0, double.MaxValue, ErrorMessage = "Value must be >= 0.")]
    public decimal Value { get; set; }
    
    [Precision(18, 2)]
    [Range(0, double.MaxValue, ErrorMessage = "Minimum order amount must be >= 0.")]
    public decimal MinimumOrderAmount { get; set; } = 0;
    
    [Precision(18, 2)]
    [Range(0, double.MaxValue, ErrorMessage = "Maximum discount amount must be >= 0.")]
    public decimal? MaximumDiscountAmount { get; set; }
    
    public int? UsageLimit { get; set; }
    
    public int UsageCount { get; set; } = 0;
    
    public bool IsActive { get; set; } = true;
    
    public bool IsForNewCustomersOnly { get; set; } = false;
    
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? EndDate { get; set; }
    
    public ICollection<Guid>? ApplicableProductIds { get; set; }
    
    public ICollection<Guid>? ApplicableCategoryIds { get; set; }
    
    public required Guid Id { get; init; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}

public enum DiscountType
{
    Percentage,
    FixedAmount,
    FreeShipping,
    BuyXGetY
}