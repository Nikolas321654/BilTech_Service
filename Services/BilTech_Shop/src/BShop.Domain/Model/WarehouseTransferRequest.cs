namespace BShop.Domain.Model;

public class WarehouseTransferRequest
{
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public virtual Shop Shop { get; set; }
    public virtual ICollection<WarehouseOrder> WarehouseOrders { get; set; }
}