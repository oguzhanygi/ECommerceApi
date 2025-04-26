using System.ComponentModel.DataAnnotations;
using ECommerceApi.Common.Attributes;
using ECommerceApi.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ECommerceApi.Models;

public class Customer : IdentityUser<Guid>, IEntity, ISoftDeletable, IHasTimestamps
{
    [MaxLength(128)] 
    [MinLength(2)] 
    public required string FirstName { get; set; }
    
    [MaxLength(128)] 
    [MinLength(2)] 
    public string? MiddleName { get; set; }
    
    [MaxLength(128)] 
    [MinLength(2)] 
    public required string LastName { get; set; }
    
    [AgeRestriction(13, 120)]
    public required DateTime DateOfBirth { get; set; }
    
    public required Gender Gender { get; set; }
    
    [MaxLength(50)]
    public string? PreferredLanguage { get; set; }
    
    public DateTime? LastLoginDate { get; set; }
    
    public int LoyaltyPoints { get; set; } = 0;
    
    public CustomerAccountStatus AccountStatus { get; set; } = CustomerAccountStatus.Active;
    
    public MarketingPreferences MarketingPreferences { get; set; } = new MarketingPreferences();
    
    public ICollection<Address> Addresses { get; init; } = new List<Address>();

    public Address? DefaultShippingAddress =>
        Addresses.FirstOrDefault(a => a.IsDefaultShipping);

    public Address? DefaultBillingAddress =>
        Addresses.FirstOrDefault(a => a.IsDefaultBilling);

    public ICollection<Order> Orders { get; init; } = new List<Order>();
    public ICollection<Review> Reviews { get; init; } = new List<Review>();
    public ICollection<Product> Wishlist { get; init; } = new List<Product>();
    
    [MaxLength(2048)]
    public string? Notes { get; set; }

    Guid IEntity.Id
    {
        get => base.Id;
        init => base.Id = value;
    }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}

public enum Gender
{
    Male,
    Female,
    Other,
    PreferNotToSay
}

public enum CustomerAccountStatus
{
    Active,
    Inactive,
    Suspended,
    Locked,
    PendingVerification
}

public class MarketingPreferences
{
    public bool EmailMarketing { get; set; } = false;
    public bool SmsMarketing { get; set; } = false;
    public bool PhoneMarketing { get; set; } = false;
    public bool PersonalizedRecommendations { get; set; } = true;
    public DateTime? LastUpdated { get; set; }
}