namespace Warehouse.Api.Models.Request;

public class CreateOrderRequest
{
    public Guid WarehouseId { get; set; }
    public List<CreateOrderItemRequest> Items { get; set; } = [];
}

public class CreateOrderItemRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
