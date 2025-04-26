using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models.Interfaces;

namespace ECommerceApi.Models;

public class Review : IEntity, ISoftDeletable, IHasTimestamps
{
    public required Guid ProductId { get; set; }
    public Product? Product { get; set; }
    
    public required Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    [MaxLength(128)]
    public string? Title { get; set; }
    
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public required int Rating { get; set; }
    
    [MaxLength(2048)]
    public string? Comment { get; set; }
    
    public bool IsVerifiedPurchase { get; set; } = false;
    
    public int HelpfulVotes { get; set; } = 0;
    
    public int NotHelpfulVotes { get; set; } = 0;
    
    public bool IsApproved { get; set; } = false;
    
    [MaxLength(1024)]
    public string? AdminReply { get; set; }
    
    public DateTime? AdminReplyDate { get; set; }
    
    public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    
    public required Guid Id { get; init; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}