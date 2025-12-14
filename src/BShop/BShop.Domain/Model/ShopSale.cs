namespace BShop.Domain.Model;

public class ShopSale
{
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public Guid GoodsId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public decimal TotalPrice { get; set; }
}