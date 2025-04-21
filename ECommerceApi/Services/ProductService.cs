using ECommerceApi.Data.Repositories.Interfaces;
using ECommerceApi.DTOs.Products;
using ECommerceApi.Models.Products;
using ECommerceApi.Services.Interfaces;

namespace ECommerceApi.Services;

public class ProductService(
    IProductRepository productRepository)
    : IProductService
{
    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await productRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await productRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId)
    {
        return await productRepository.GetProductsByCategoryAsync(categoryId);
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
    {
        return await productRepository.SearchProductsAsync(searchTerm);
    }

    public async Task<Product?> CreateProductAsync(CreateProductDto dto)
    {
        return await productRepository.AddAsync(dto);
    }

    public async Task<Product?> UpdateProductAsync(Guid id, UpdateProductDto dto)
    {
        return await productRepository.UpdateAsync(id, dto);
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        return await productRepository.DeleteAsync(id);
    }

    public async Task<bool> RestoreProductAsync(Guid id)
    {
        return await productRepository.RestoreAsync(id);
    }

    public async Task<bool> UpdateProductStockAsync(Guid productId, int quantityChange)
    {
        var product = await productRepository.GetByIdAsync(productId);
        if (product == null) return false;

        product.StockQuantity += quantityChange;
        await productRepository.UpdateAsync(product);
        return true;
    }
}