using BShop.Application.Models;

namespace BShop.Application.Models;

public class ShopApi
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<ShopCheckApi> ShopSales { get; set; }
    public ICollection<WarehouseTransferRequestApi> WarehouseTransferRequests { get; set; }
    public ICollection<ShopStorageApi> Inventory { get; set; }
}