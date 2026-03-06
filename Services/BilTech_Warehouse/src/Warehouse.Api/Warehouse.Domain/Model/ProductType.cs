namespace Warehouse.Domain.Model;

public class ProductType
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
}