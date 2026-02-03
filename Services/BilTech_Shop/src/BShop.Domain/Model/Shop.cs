namespace BShop.Domain.Model;

public class Shop
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsDeleted { get; set; }

    public virtual ICollection<ShopCheck>? ShopSales { get; set; }
    public virtual ICollection<WarehouseTransferRequest>? WarehouseTransferRequests { get; set; }
    public virtual ICollection<ShopStorage>? Inventory { get; set; }
}