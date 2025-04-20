using System.ComponentModel.DataAnnotations;
using ECommerceApi.Dtos.Interfaces;
using ECommerceApi.Models.Products;

namespace ECommerceApi.DTOs.Products;

public class UpdateProductDto : IUpdateDto<Product>
{
    [MaxLength(128), MinLength(2)]
    public required string Name { get; set; }
    
    [MaxLength(1024)]
    public string? Description { get; set; }
    
    [Range(0.0, Double.MaxValue, ErrorMessage = "Price must be > 0.")]
    public required decimal Price { get; set; }
    
    [MaxLength(128), MinLength(2)]
    public required string Brand { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Stock must be >= 0.")]
    public int StockQuantity { get; set; }
    
    public required Guid CategoryId { get; set; }
    
    public void ApplyUpdatesToEntity(Product product)
    {
        product.Name = Name;
        product.Description = Description;
        product.Price = Price;
        product.Brand = Brand;
        product.StockQuantity = StockQuantity;
        product.CategoryId = CategoryId;
        product.UpdatedAt = DateTime.UtcNow;
    }
}