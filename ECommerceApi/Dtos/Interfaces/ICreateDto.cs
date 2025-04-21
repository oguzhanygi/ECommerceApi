namespace ECommerceApi.Dtos.Interfaces;

public interface ICreateDto<T> where T : class
{
    T ToEntity();
}