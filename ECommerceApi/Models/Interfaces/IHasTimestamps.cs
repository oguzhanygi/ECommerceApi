namespace ECommerceApi.Models.Interfaces;

public interface IHasTimestamps
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}