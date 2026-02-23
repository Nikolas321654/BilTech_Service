using BShop.Domain.Interfaces;
using BShop.Domain.Model;

namespace BShop.Application.Builders;

public class WarehouseRequestBuilder
{
    private WarehouseTransferRequest WarehouseTransferRequest { get; } = new();

    public WarehouseTransferRequest FinishBuild() => WarehouseTransferRequest;

    public WarehouseRequestBuilder AddId()
    {
        WarehouseTransferRequest.Id = Guid.NewGuid();
        return this;
    }

    public WarehouseRequestBuilder AddShopId(Guid shopId)
    {
        if (shopId == Guid.Empty) throw new ArgumentException("Shop Id cannot be empty");
        
        WarehouseTransferRequest.ShopId = shopId;
        return this;
    }

    public WarehouseRequestBuilder AddCreatedAt()
    {
        WarehouseTransferRequest.CreatedAt = DateTime.UtcNow;
        return this;
    }

    public WarehouseRequestBuilder AddDeliveredAt(DateTime? deliveredAt)
    {
        WarehouseTransferRequest.DeliveredAt = deliveredAt;
        return this;
    }

    public WarehouseRequestBuilder AddAtIsDeleted()
    {
        WarehouseTransferRequest.IsDeleted = false;
        return this;
    }
}