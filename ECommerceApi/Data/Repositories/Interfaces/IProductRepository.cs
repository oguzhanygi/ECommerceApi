using ECommerceApi.Models.Products;

namespace ECommerceApi.Data.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId);
    Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
}