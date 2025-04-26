using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Models;

public class Order : IEntity, ISoftDeletable, IHasTimestamps
{
    [MaxLength(32)]
    public required string OrderNumber { get; set; }
    
    public required OrderStatus Status { get; set; } = OrderStatus.Pending;
    
    public required PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    
    [MaxLength(64)]
    public string? PaymentMethod { get; set; }
    
    [MaxLength(128)]
    public string? PaymentTransactionId { get; set; }
    
    [MaxLength(64)]
    public string? ShippingMethod { get; set; }
    
    [MaxLength(128)]
    public string? TrackingNumber { get; set; }
    
    public required Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public Guid? ShippingAddressId { get; set; }
    public Address? ShippingAddress { get; set; }
    
    public Guid? BillingAddressId { get; set; }
    public Address? BillingAddress { get; set; }
    
    [Precision(18, 2)]
    public required decimal Subtotal { get; set; }
    
    [Precision(18, 2)]
    public decimal ShippingCost { get; set; } = 0;
    
    [Precision(18, 2)]
    public decimal TaxAmount { get; set; } = 0;
    
    [Precision(18, 2)]
    public decimal DiscountAmount { get; set; } = 0;
    
    [Precision(18, 2)]
    public required decimal TotalAmount { get; set; }
    
    [MaxLength(32)]
    public string? DiscountCode { get; set; }
    
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? ProcessedDate { get; set; }
    
    public DateTime? ShippedDate { get; set; }
    
    public DateTime? DeliveredDate { get; set; }
    
    [MaxLength(1024)]
    public string? Notes { get; set; }
    
    [MaxLength(1024)]
    public string? CustomerNotes { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    
    public required Guid Id { get; init; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled,
    Returned,
    Refunded
}

public enum PaymentStatus
{
    Pending,
    Authorized,
    Paid,
    Failed,
    Refunded,
    PartiallyRefunded
}

public class OrderItem : IEntity
{
    public required Guid OrderId { get; set; }
    public Order? Order { get; set; }
    
    public required Guid ProductId { get; set; }
    public Product? Product { get; set; }
    
    [MaxLength(128)]
    public required string ProductName { get; set; }
    
    [MaxLength(64)]
    public string? SKU { get; set; }
    
    [Precision(18, 2)]
    public required decimal UnitPrice { get; set; }
    
    public int Quantity { get; set; } = 1;
    
    [Precision(18, 2)]
    public decimal Discount { get; set; } = 0;
    
    [Precision(18, 2)]
    public required decimal TotalPrice { get; set; }
    
    [MaxLength(512)]
    public string? ProductOptions { get; set; }
    
    public required Guid Id { get; init; }
}