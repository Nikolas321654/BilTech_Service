namespace Warehouse.Api.Models.Request;

public class Order
{
    Guid WarehouseId { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime? CompletedAt { get; set; }
    DateTime? DeliveredAt { get; set; }
    bool IsDeleted { get; set; } = false;
}