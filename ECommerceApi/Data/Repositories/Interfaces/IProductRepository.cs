using ECommerceApi.Models.Products;

namespace ECommerceApi.Data.Repositories.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task<bool> DeleteAsync(Guid id);
}