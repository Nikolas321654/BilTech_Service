namespace BShop.Application.Models;

public class SoldProductApi
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int ProductCount { get; set; }
    public decimal TotalPrice { get; set; }

    public ProductApi Product { get; set; }
    public ShopCheckApi ShopCheck { get; set; }
}