namespace BShop.Application.Models;

public class WarehouseOrderApi
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int ProductCount { get; set; }

    public ProductApi Product { get; set; }
    public WarehouseTransferRequestApi WarehouseTransferRequest { get; set; }
}