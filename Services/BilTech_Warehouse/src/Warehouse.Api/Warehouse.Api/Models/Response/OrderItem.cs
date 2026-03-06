namespace Warehouse.Api.Models.Response;

public class OrderItem
{
    Guid OrderId { get; set; }
    Guid ProductId { get; set; }
    int Quantity { get; set; }
}