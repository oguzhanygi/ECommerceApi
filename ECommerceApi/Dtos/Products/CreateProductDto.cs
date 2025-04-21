using System.ComponentModel.DataAnnotations;
using ECommerceApi.Dtos.Interfaces;
using ECommerceApi.Models.Products;

namespace ECommerceApi.DTOs.Products;

public class CreateProductDto : ICreateDto<Product>
{
    [Required(ErrorMessage = "Product name is required")]
    [MaxLength(128, ErrorMessage = "Name cannot exceed 128 characters")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
    public required string Name { get; set; }

    [MaxLength(1024, ErrorMessage = "Description cannot exceed 1024 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public required decimal Price { get; set; }

    [Required(ErrorMessage = "Brand is required")]
    [MaxLength(128, ErrorMessage = "Brand cannot exceed 128 characters")]
    [MinLength(2, ErrorMessage = "Brand must be at least 2 characters")]
    public required string Brand { get; set; }

    [Required(ErrorMessage = "Stock quantity is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative")]
    public int StockQuantity { get; set; }

    [Required(ErrorMessage = "Category ID is required")]
    public required Guid CategoryId { get; set; }

    public Product ToEntity()
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = this.Name,
            Description = this.Description,
            Price = this.Price,
            Brand = this.Brand,
            StockQuantity = this.StockQuantity,
            CategoryId = this.CategoryId
        };
    }
}