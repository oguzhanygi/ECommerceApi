using ECommerceApi.Data.Repositories.Interfaces;
using ECommerceApi.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Data.Repositories;

public class Repository<T>(ECommerceDbContext context) : IRepository<T> where T : class, IEntity
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<T?> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null) return false;

        if (entity is ISoftDeletable softDeletable)
            softDeletable.IsDeleted = true;
        // If not soft deletable, do physical delete
        else
            _dbSet.Remove(entity);

        // Update timestamp
        if (entity is IHasTimestamps withTimestamps) withTimestamps.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RestoreAsync(Guid id)
    {
        var entity = await _dbSet
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(e => e.Id == id);
        if (entity is null || entity is not ISoftDeletable softDeletable)
            return false;

        softDeletable.IsDeleted = false;

        // Update timestamp
        if (entity is IHasTimestamps withTimestamps) withTimestamps.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return true;
    }
}