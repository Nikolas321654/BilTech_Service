namespace BShop.Models;

public class ShopStorageApi
{
    public Guid ShopId { get; set; }
    public Guid ProductId { get; set; }
    public int ProductCount { get; set; }
    public decimal ProductPrice { get; set; }

    public virtual ShopApi Shop { get; set; }
    public virtual ProductApi Product { get; set; }
}