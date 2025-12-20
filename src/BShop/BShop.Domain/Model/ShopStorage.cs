namespace BShop.Domain.Model;

public class ShopStorage
{
    public Guid ShopId { get; set; }
    public Guid ProductId { get; set; }
    public int ProductCount { get; set; }
    public decimal ProductPrice { get; set; }

    public virtual Shop Shop { get; set; }
    public virtual Product Product { get; set; }
}