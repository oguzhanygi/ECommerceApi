using ECommerceApi.Dtos.Interfaces;

namespace ECommerceApi.Data.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> AddAsync<TDto>(TDto dto) where TDto : ICreateDto<T>;
    Task UpdateAsync(T entity);
    Task<T?> UpdateAsync<TDto>(Guid id, TDto dto) where TDto : IUpdateDto<T>;
    Task<bool> DeleteAsync(Guid id);
    Task<bool> RestoreAsync(Guid id);
}