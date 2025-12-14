namespace BShop.Domain.Model;

public class ShopStorageInventory
{
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public Guid GoodsId { get; set; }
    public int Quantity { get; set; }
    
    public virtual Shop Shop { get; set; }
    public virtual Product Goods { get; set; }
}