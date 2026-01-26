namespace BShop.Domain.Model;

public class SoldProduct
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int ProductCount { get; set; }
    public decimal TotalPrice { get; set; }

    public virtual Product Product { get; set; }
    public virtual ShopCheck ShopCheck { get; set; }
}