namespace ECommerceApi.Models.Interfaces;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
}