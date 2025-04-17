using ECommerceApi.Data.Repositories.Interfaces;
using ECommerceApi.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Data.Repositories;

public class ProductRepository(ECommerceDbContext context) : IProductRepository
{
    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await context.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId)
    {
        return await context.Products
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        var existing = await context.Products.FindAsync(product.Id);
        if (existing is null) return;
        context.Entry(existing).CurrentValues.SetValues(product);
        existing.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await GetByIdAsync(id);
        if (product is null) return false;
        product.IsDeleted = true;
        product.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
        return true;
    }
}