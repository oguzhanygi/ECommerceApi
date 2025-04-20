using ECommerceApi.DTOs.Products;
using ECommerceApi.Models.Products;

namespace ECommerceApi.Services.Interfaces;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId);
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
    Task<Product> CreateProductAsync(Product product);
    Task<Product?> UpdateProductAsync(Guid id, UpdateProductDto dto);
    Task<bool> DeleteProductAsync(Guid id);
    Task<bool> RestoreProductAsync(Guid id);
    Task<bool> UpdateProductStockAsync(Guid productId, int quantityChange);
}