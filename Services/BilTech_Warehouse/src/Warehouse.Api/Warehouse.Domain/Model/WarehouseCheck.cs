namespace Warehouse.Domain.Model;

public class WarehouseCheck
{
    public Guid Id { get; set; }
    public Guid WarehouseId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public decimal TotalPrice { get; set; }
}