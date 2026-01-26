namespace BShop.Models;

public class ShopCheckApi
{
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public decimal TotalPrice { get; set; }

    public virtual ShopApi Shop { get; set; }
    public virtual ICollection<SoldProductApi> SaledProducts { get; set; }
}