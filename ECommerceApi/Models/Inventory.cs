using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models.Interfaces;

namespace ECommerceApi.Models;

public class Inventory : IEntity, IHasTimestamps
{
    public required Guid ProductId { get; set; }
    public Product? Product { get; set; }
    
    [MaxLength(64)]
    public string? SKU { get; set; }
    
    [MaxLength(64)]
    public string? Location { get; set; }
    
    public int QuantityInStock { get; set; } = 0;
    
    public int ReorderLevel { get; set; } = 10;
    
    public int ReorderQuantity { get; set; } = 20;
    
    public bool IsLowStockAlertEnabled { get; set; } = true;
    
    public bool IsOutOfStockSalesAllowed { get; set; } = false;
    
    public InventoryStatus Status { get; set; } = InventoryStatus.InStock;
    
    [MaxLength(256)]
    public string? Notes { get; set; }
    
    public ICollection<InventoryTransaction> Transactions { get; set; } = new List<InventoryTransaction>();
    
    public required Guid Id { get; init; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public enum InventoryStatus
{
    InStock,
    LowStock,
    OutOfStock,
    Discontinued,
    OnOrder,
    PreOrder
}

public class InventoryTransaction : IEntity, IHasTimestamps
{
    public required Guid InventoryId { get; set; }
    public Inventory? Inventory { get; set; }
    
    public required TransactionType Type { get; set; }
    
    public required int Quantity { get; set; }
    
    [MaxLength(64)]
    public string? ReferenceNumber { get; set; }
    
    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }
    
    [MaxLength(64)]
    public string? PerformedBy { get; set; }
    
    [MaxLength(256)]
    public string? Notes { get; set; }
    
    public required Guid Id { get; init; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public enum TransactionType
{
    Received,
    Sold,
    Returned,
    Adjusted,
    Damaged,
    Reserved,
    Transferred
}