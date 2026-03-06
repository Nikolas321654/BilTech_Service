namespace Warehouse.Domain.Model;

public enum OrderStatus
{
    Packing,
    LeftWarehouse,
    Completed,
    Cancelled
}

public class Order
{
    public Guid Id { get; set; }
    public Guid WarehouseId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public OrderStatus Status { get; set; }
    public bool IsDeleted { get; set; } = false;

    public List<OrderItem> Items { get; set; } = [];
}