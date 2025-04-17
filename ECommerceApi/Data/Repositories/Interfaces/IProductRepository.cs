using ECommerceApi.Models.Products;

namespace ECommerceApi.Data.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId);
}