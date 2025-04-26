using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Models;

public class Cart : IEntity, IHasTimestamps
{
    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    [MaxLength(128)]
    public string? SessionId { get; set; }
    
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    
    [Precision(18, 2)]
    public decimal Subtotal { get; set; } = 0;
    
    [MaxLength(32)]
    public string? CouponCode { get; set; }
    
    [Precision(18, 2)]
    public decimal DiscountAmount { get; set; } = 0;
    
    public DateTime LastActivity { get; set; } = DateTime.UtcNow;
    
    public bool IsActive { get; set; } = true;
    
    [MaxLength(1024)]
    public string? Notes { get; set; }
    
    public required Guid Id { get; init; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public class CartItem : IEntity
{
    public required Guid CartId { get; set; }
    public Cart? Cart { get; set; }
    
    public required Guid ProductId { get; set; }
    public Product? Product { get; set; }
    
    public int Quantity { get; set; } = 1;
    
    [Precision(18, 2)]
    public required decimal UnitPrice { get; set; }
    
    [Precision(18, 2)]
    public decimal DiscountAmount { get; set; } = 0;
    
    [MaxLength(512)]
    public string? SelectedOptions { get; set; }
    
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
    
    public required Guid Id { get; init; }
}