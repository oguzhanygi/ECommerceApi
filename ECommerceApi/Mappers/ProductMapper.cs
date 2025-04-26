using ECommerceApi.DTOs.Products;
using ECommerceApi.Models;

namespace ECommerceApi.Mappers;

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
            SalePrice = product.SalePrice,
            StockQuantity = product.StockQuantity,
            SKU = product.SKU,
            Weight = product.Weight,
            Length = product.Length,
            Width = product.Width,
            Height = product.Height,
            WeightUnit = product.WeightUnit,
            DimensionUnit = product.DimensionUnit,
            IsAvailable = product.IsAvailable,
            IsFeatured = product.IsFeatured,
            ReleaseDate = product.ReleaseDate,
            ImageUrls = product.ImageUrls,
            Tags = product.Tags,
            AverageRating = product.AverageRating,
            CategoryId = product.CategoryId,
            CategoryName = product.Category?.Name,
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
            SalePrice = dto.SalePrice,
            StockQuantity = dto.StockQuantity,
            SKU = dto.SKU,
            Weight = dto.Weight,
            Length = dto.Length,
            Width = dto.Width,
            Height = dto.Height,
            WeightUnit = dto.WeightUnit,
            DimensionUnit = dto.DimensionUnit,
            IsAvailable = dto.IsAvailable,
            IsFeatured = dto.IsFeatured,
            ReleaseDate = dto.ReleaseDate,
            ImageUrls = dto.ImageUrls?.ToList() ?? new List<string>(),
            Tags = dto.Tags?.ToList() ?? new List<string>(),
            CategoryId = dto.CategoryId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
    }

    public static void ApplyUpdateToProduct(Product product, UpdateProductDto dto)
    {
        if (dto.Name != null) product.Name = dto.Name;
        if (dto.Description != null) product.Description = dto.Description;
        if (dto.Brand != null) product.Brand = dto.Brand;
        if (dto.Price.HasValue) product.Price = dto.Price.Value;
        if (dto.SalePrice.HasValue) product.SalePrice = dto.SalePrice.Value;
        if (dto.StockQuantity.HasValue) product.StockQuantity = dto.StockQuantity.Value;
        if (dto.SKU != null) product.SKU = dto.SKU;
        if (dto.Weight.HasValue) product.Weight = dto.Weight.Value;
        if (dto.Length.HasValue) product.Length = dto.Length.Value;
        if (dto.Width.HasValue) product.Width = dto.Width.Value;
        if (dto.Height.HasValue) product.Height = dto.Height.Value;
        if (dto.WeightUnit != null) product.WeightUnit = dto.WeightUnit;
        if (dto.DimensionUnit != null) product.DimensionUnit = dto.DimensionUnit;
        if (dto.IsAvailable.HasValue) product.IsAvailable = dto.IsAvailable.Value;
        if (dto.IsFeatured.HasValue) product.IsFeatured = dto.IsFeatured.Value;
        if (dto.ReleaseDate.HasValue) product.ReleaseDate = dto.ReleaseDate.Value;
        if (dto.ImageUrls != null) product.ImageUrls = dto.ImageUrls.ToList();
        if (dto.Tags != null) product.Tags = dto.Tags.ToList();
        if (dto.CategoryId.HasValue) product.CategoryId = dto.CategoryId.Value;
        
        product.UpdatedAt = DateTime.UtcNow;
    }
}