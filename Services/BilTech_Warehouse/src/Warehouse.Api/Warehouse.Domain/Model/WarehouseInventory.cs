namespace Warehouse.Domain.Model;

public class WarehouseInventory
{
    public Guid ProductId { get; set; }
    public Guid WarehouseId { get; set; }
    public int Quantity { get; set; }
    public decimal ProductPrice { get; set; }
}