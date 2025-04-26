using ECommerceApi.Data.Repositories.Interfaces;
using ECommerceApi.DTOs.Products;
using ECommerceApi.Mappers;
using ECommerceApi.Services.Interfaces;

namespace ECommerceApi.Services;

public class ProductService(
    IProductRepository productRepository)
    : IProductService
{
    public async Task<ProductResponseDto?> GetProductByIdAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);
        return product is null ? null : ProductMapper.ToResponseDto(product);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
    {
        var products = await productRepository.GetAllAsync();
        return products.Select(ProductMapper.ToResponseDto)!;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetProductsByCategoryAsync(Guid categoryId)
    {
        var products = await productRepository.GetProductsByCategoryAsync(categoryId);
        return products.Select(ProductMapper.ToResponseDto)!;
    }

    public async Task<IEnumerable<ProductResponseDto>> SearchProductsAsync(string searchTerm)
    {
        var products = await productRepository.SearchProductsAsync(searchTerm);
        return products.Select(ProductMapper.ToResponseDto)!;
    }

    public async Task<ProductResponseDto?> CreateProductAsync(CreateProductDto dto)
    {
        var product = ProductMapper.ToProduct(dto);
        await productRepository.AddAsync(product);
        return ProductMapper.ToResponseDto(product);
    }

    public async Task<ProductResponseDto?> UpdateProductAsync(Guid id, UpdateProductDto dto)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null) return null;
        ProductMapper.ApplyUpdateToProduct(product, dto);
        await productRepository.UpdateAsync(product);
        return ProductMapper.ToResponseDto(product);
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