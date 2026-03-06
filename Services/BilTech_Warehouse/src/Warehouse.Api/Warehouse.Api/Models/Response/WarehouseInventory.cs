namespace Warehouse.Api.Models.Response;

public class WarehouseInventory
{
    Guid ProductId { get; set; }
    Guid WarehouseId { get; set; }
    int Quantity { get; set; }
}