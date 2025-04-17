using ECommerceApi.Data.Repositories.Interfaces;
using ECommerceApi.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Data.Repositories;

public class ProductRepository(ECommerceDbContext context) : IProductRepository
{

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId)
    {
        return await context.Products
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }
}