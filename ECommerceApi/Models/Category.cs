using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models.Interfaces;

namespace ECommerceApi.Models;

public class Category : IEntity, ISoftDeletable, IHasTimestamps
{
    [MaxLength(128)] 
    [MinLength(2)] 
    public required string Name { get; set; }
    
    [MaxLength(512)]
    public string? Description { get; set; }
    
    [MaxLength(256)]
    public string? ImageUrl { get; set; }
    
    public int DisplayOrder { get; set; } = 0;
    
    public bool IsActive { get; set; } = true;
    
    [MaxLength(128)]
    public string? Slug { get; set; }
    
    [MaxLength(256)]
    public string? MetaTitle { get; set; }
    
    [MaxLength(512)]
    public string? MetaDescription { get; set; }
    
    [MaxLength(256)]
    public string? MetaKeywords { get; set; }

    public Guid? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public ICollection<Category> SubCategories { get; init; } = new List<Category>();
    public ICollection<Product> Products { get; init; } = new List<Product>();
    public required Guid Id { get; init; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}