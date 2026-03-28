namespace BShop.Application.Models;

public class ProductApi
{
    public Guid Id { get; set; }
    public Guid TypeId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<ProductTypeApi> ProductTypes { get; set; }
}