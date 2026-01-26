namespace BShop.Models;

public class ShopApi
{
    public Guid Id { get; set; }
    public Guid? EmployeeId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsDeleted { get; set; }

    public virtual ICollection<ShopCheckApi> ShopSales { get; set; }
    public virtual ICollection<WarehouseTransferRequestApi> WarehouseTransferRequests { get; set; }
    public virtual ICollection<ShopStorageApi> Inventory { get; set; }
    public virtual WorkerApi Employee { get; set; }
}