namespace Warehouse.Api.Models.Response;

public class Order
{
    Guid Id { get; set; }
    Guid WarehouseId { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime? CompletedAt { get; set; }
    DateTime? DeliveredAt { get; set; }
    bool IsDeleted { get; set; } = false;
}