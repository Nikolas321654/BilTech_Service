namespace BShop.Domain.Model;

public class ShopSale
{
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public decimal TotalPrice { get; set; }
    
    public virtual Shop Shop { get; set; }
    public virtual Product Goods { get; set; }
}