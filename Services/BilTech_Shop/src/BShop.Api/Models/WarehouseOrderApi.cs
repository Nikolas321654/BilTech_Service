namespace BShop.Models;

public class WarehouseOrderApi
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int ProductCount { get; set; }

    public virtual ProductApi Product { get; set; }
    public virtual WarehouseTransferRequestApi WarehouseTransferRequest { get; set; }
}