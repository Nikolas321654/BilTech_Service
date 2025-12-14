namespace BShop.Domain.Model;

public class ProductType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
}