namespace ECommerceApi.Dtos.Interfaces;

public interface IUpdateDto<T> where T : class
{
    void ApplyUpdatesToEntity(T entity);
}