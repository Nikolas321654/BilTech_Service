namespace Warehouse.Api.Models.Request;

public class OrderItem
{
    Guid OrderId { get; set; }
    Guid ProductId { get; set; }
    int Quantity { get; set; }
}