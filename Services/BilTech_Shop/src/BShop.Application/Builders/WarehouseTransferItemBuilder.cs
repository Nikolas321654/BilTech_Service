using BShop.Domain.Model;

namespace BShop.Application.Builders;

public class WarehouseTransferItemBuilder
{
    private WarehouseOrder WarehouseOrder { get; } = new();

    public WarehouseOrder FinishBuild() => WarehouseOrder;

    public WarehouseTransferItemBuilder AddOrderId(Guid orderId)
    {
        if (orderId == Guid.Empty) throw new ArgumentException("Order Id cannot be empty");
        WarehouseOrder.OrderId = orderId;
        return this;
    }

    public WarehouseTransferItemBuilder AddProductId(Guid productId)
    {
        if (productId == Guid.Empty) throw new ArgumentException("Shop Id cannot be empty");

        WarehouseOrder.ProductId = productId;
        return this;
    }

    public WarehouseTransferItemBuilder AddProductCount(int productCount)
    {
        if(productCount <= 0) throw new ArgumentException("Product count must be greater than 0");
        WarehouseOrder.ProductCount = productCount;
        return this;
    }
}