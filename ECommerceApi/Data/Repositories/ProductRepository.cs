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

    public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return Enumerable.Empty<Product>();

        var term = searchTerm.Trim().ToLower();

        return await _context.Products
            .Where(p => p.Name.ToLower().Contains(term) ||
                        (p.Description != null && p.Description.ToLower().Contains(term)) ||
                        p.Brand.ToLower().Contains(term))
            .Include(p => p.Category)
            .ToListAsync();
    }
}