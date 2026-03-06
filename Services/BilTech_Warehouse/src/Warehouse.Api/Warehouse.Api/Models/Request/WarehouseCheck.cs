namespace Warehouse.Api.Models.Request;

public class WarehouseCheck
{
    Guid Id { get; set; }
    Guid WarehouseId { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    bool IsDeleted { get; set; } = false;
}