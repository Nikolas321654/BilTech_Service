using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BShop.Infrastructure.Configurations;

public class WarehouseOrderConfiguration : IEntityTypeConfiguration<WarehouseOrder>
{
    public void Configure(EntityTypeBuilder<WarehouseOrder> builder)
    {
        builder.HasKey(x => new { x.OrderId, x.ProductId });

        builder
            .HasOne(x => x.WarehouseTransferRequest)
            .WithMany(x => x.WarehouseOrders)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId);


        builder.HasIndex(x => x.OrderId);
        builder.HasIndex(x => x.ProductId);
    }
}