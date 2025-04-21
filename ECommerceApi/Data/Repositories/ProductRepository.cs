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

    public async Task<IEnumerable<Product>> SearchProductsAsync(
        string searchTerm,
        int page = 1,
        int pageSize = 20
    )
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return Enumerable.Empty<Product>();

        var term = searchTerm.Trim().ToLower();

        return await _context.Products
            .Where(p => p.Name.ToLower().Contains(term) ||
                        (p.Description != null && p.Description.ToLower().Contains(term)) ||
                        p.Brand.ToLower().Contains(term))
            .OrderBy(p => p.Name.ToLower().Contains(term) ? 0 : 1)
            .ThenBy(p => p.Brand.ToLower().Contains(term) ? 0 : 1)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(p => p.Category)
            .ToListAsync();
    }
}