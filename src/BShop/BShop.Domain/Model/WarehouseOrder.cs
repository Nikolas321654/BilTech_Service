namespace BShop.Domain.Model;

public class WarehouseOrder
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int ProductCount { get; set; }

    public virtual Product Product { get; set; }
    public virtual WarehouseTransferRequest WarehouseTransferRequest { get; set; }
}