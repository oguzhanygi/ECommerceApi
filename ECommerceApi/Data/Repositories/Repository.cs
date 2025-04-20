using ECommerceApi.Data.Repositories.Interfaces;
using ECommerceApi.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Data.Repositories;

public class Repository<T>(ECommerceDbContext context) : IRepository<T> where T : class
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

    async Task<T?> IRepository<T>.UpdateAsync<TDto>(Guid id, TDto dto)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) return null;

        dto.ApplyUpdatesToEntity(entity);
        
        if (entity is IHasTimestamps withTimestamps)
        {
            withTimestamps.UpdatedAt = DateTime.UtcNow;
        }

        await UpdateAsync(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null) return false;

        if (entity is ISoftDeletable softDeletable)
        {
            softDeletable.IsDeleted = true;
        }
        // If not soft deletable, do physical delete
        else
        {
            _dbSet.Remove(entity);
        }

        // Update timestamp
        if (entity is IHasTimestamps withTimestamps)
        {
            withTimestamps.UpdatedAt = DateTime.UtcNow;
        }
        
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RestoreAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null || entity is not ISoftDeletable softDeletable) 
            return false;

        softDeletable.IsDeleted = false;
    
        // Update timestamp
        if (entity is IHasTimestamps withTimestamps)
        {
            withTimestamps.UpdatedAt = DateTime.UtcNow;
        }
    
        await context.SaveChangesAsync();
        return true;
    }
}