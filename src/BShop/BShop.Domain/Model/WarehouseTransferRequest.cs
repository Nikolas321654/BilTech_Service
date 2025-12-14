namespace BShop.Domain.Model;

public class WarehouseTransferRequest
{
    public Guid Id { get; set; }
    public Guid GoodsId { get; set; }
    public Guid WarehouseId { get; set; }
    public Guid ShopId { get; set; }
    public int Quantity { get; set; }
    public string OrderStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}