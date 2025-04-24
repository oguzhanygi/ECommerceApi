using ECommerceApi.DTOs.Products;

namespace ECommerceApi.Services.Interfaces;

public interface IProductService
{
    Task<ProductResponseDto?> GetProductByIdAsync(Guid id);
    Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
    Task<IEnumerable<ProductResponseDto>> GetProductsByCategoryAsync(Guid categoryId);
    Task<IEnumerable<ProductResponseDto>> SearchProductsAsync(string searchTerm);
    Task<ProductResponseDto?> CreateProductAsync(CreateProductDto dto);
    Task<ProductResponseDto?> UpdateProductAsync(Guid id, UpdateProductDto dto);
    Task<bool> DeleteProductAsync(Guid id);
    Task<bool> RestoreProductAsync(Guid id);
    Task<bool> UpdateProductStockAsync(Guid productId, int quantityChange);
}