namespace BShop.Models;

public class ProductApi
{
    public Guid Id { get; set; }
    public Guid TypeId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public virtual ICollection<ProductTypeApi> ProductTypes { get; set; }
}