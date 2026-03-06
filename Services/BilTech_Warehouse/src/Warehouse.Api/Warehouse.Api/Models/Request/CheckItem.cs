namespace Warehouse.Api.Models.Request;

public class CheckItem
{
    public Guid CheckId { get; set; }
    public Guid WarehouseId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}