namespace BShop.Application.Models;


public class ShopStorageApi
{
    public Guid ShopId { get; set; }
    public Guid ProductId { get; set; }
    public int ProductCount { get; set; }
    public decimal ProductPrice { get; set; }

    public ShopApi Shop { get; set; }
    public ProductApi Product { get; set; }
}