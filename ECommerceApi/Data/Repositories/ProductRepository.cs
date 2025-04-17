using ECommerceApi.Data.Repositories.Interfaces;
using ECommerceApi.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Data.Repositories;

public class ProductRepository(ECommerceDbContext context) :
    Repository<Product>(context), IProductRepository
{
    private readonly ECommerceDbContext _context = context;

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId)
    {
        return await _context.Products
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }
}