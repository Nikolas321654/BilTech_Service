namespace BShop.Domain.Model;

public class ProductType
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public bool IsDeleted { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}