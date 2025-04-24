using ECommerceApi.DTOs.Products;
using ECommerceApi.Models.Products;

namespace ECommerceApi.Mappers.Products;

public static class ProductMapper
{
    public static ProductResponseDto? ToResponseDto(Product product)
    {
        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Brand = product.Brand,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            CategoryId = product.CategoryId,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }

    public static Product ToProduct(CreateProductDto dto)
    {
        return new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Brand = dto.Brand,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            CategoryId = dto.CategoryId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
    }

    public static void ApplyUpdateToProduct(Product product, UpdateProductDto dto)
    {
        product.Name = dto.Name ?? product.Name;
        product.Description = dto.Description ?? product.Description;
        product.Brand = dto.Brand ?? product.Brand;
        product.Price = dto.Price ?? product.Price;
        product.StockQuantity = dto.StockQuantity ?? product.StockQuantity;
        product.CategoryId = dto.CategoryId ?? product.CategoryId;
        product.UpdatedAt = DateTime.UtcNow;
    }
}