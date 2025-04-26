namespace ECommerceApi.DTOs.Products;

public class ProductResponseDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    public required string Brand { get; set; }
    public string? SKU { get; set; }
    public int StockQuantity { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Length { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    public string? WeightUnit { get; set; }
    public string? DimensionUnit { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public ICollection<string>? ImageUrls { get; set; }
    public ICollection<string>? Tags { get; set; }
    public double? AverageRating { get; set; }
    public required Guid CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}