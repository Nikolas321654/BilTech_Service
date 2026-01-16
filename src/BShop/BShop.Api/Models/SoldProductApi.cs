namespace BShop.Models;

public class SoldProductApi
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int ProductCount { get; set; }
    public decimal TotalPrice { get; set; }

    public virtual ProductApi Product { get; set; }
    public virtual ShopCheckApi ShopCheck { get; set; }
}