namespace BShop.Models;

public class ProductTypeApi
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public bool IsDeleted { get; set; }
    
    public virtual ICollection<ProductApi> Products { get; set; }
}