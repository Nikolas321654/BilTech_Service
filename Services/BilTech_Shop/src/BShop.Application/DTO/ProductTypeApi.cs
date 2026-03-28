namespace BShop.Application.Models;

public class ProductTypeApi
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public bool IsDeleted { get; set; }
    
    public ICollection<ProductApi> Products { get; set; }
}