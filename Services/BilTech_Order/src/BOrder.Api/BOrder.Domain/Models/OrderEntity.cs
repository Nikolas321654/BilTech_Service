namespace BOrder.Domain.Models;

public class OrderEntity
{
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Status { get; set; }
    public List<OrderItemEntity> Items { get; set; }
}