using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models.Interfaces;
using ECommerceApi.Models.Products;

namespace ECommerceApi.Models.Categories;

public class Category : ISoftDeletable, IHasTimestamps
{
    public required Guid Id { get; init; }

    [MaxLength(128)] [MinLength(2)] public required string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public ICollection<Category>? SubCategories { get; set; } = new List<Category>();
    public ICollection<Product>? Products { get; set; } = new List<Product>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}