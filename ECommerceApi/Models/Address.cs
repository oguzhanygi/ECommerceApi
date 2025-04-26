using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models.Interfaces;

namespace ECommerceApi.Models;

public class Address : IEntity, ISoftDeletable, IHasTimestamps
{
    [MaxLength(128)] public required string Street { get; set; }
    [MaxLength(128)] public required string Neighborhood { get; set; }
    [MaxLength(128)] public required string County { get; set; }

    [MaxLength(64)] public required string City { get; set; }

    [MaxLength(20)] public required string PostalCode { get; set; }

    [MaxLength(64)] public required string Country { get; set; }

    public bool IsDefaultShipping { get; set; }
    public bool IsDefaultBilling { get; set; }
    public required Guid CustomerId { get; init; }
    public required Customer Customer { get; set; }
    public required Guid Id { get; init; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}