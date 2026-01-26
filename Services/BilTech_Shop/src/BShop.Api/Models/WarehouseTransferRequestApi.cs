namespace BShop.Models;

public class WarehouseTransferRequestApi
{
    public Guid Id { get; set; }
    public Guid WarehouseId { get; set; }
    public Guid ShopId { get; set; }
    public string Status { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public virtual ShopApi Shop { get; set; }
    public virtual ICollection<WarehouseOrderApi> WarehouseOrders { get; set; }
}