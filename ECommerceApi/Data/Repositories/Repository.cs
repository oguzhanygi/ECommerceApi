using ECommerceApi.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Data.Repositories;

public class Repository<T>(ECommerceDbContext context) : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null) return false;

        _dbSet.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}