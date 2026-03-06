using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Model;

namespace Warehouse.Infrastructure.Configurations;

public class WarehouseInventoryConfiguration : IEntityTypeConfiguration<WarehouseInventory>
{
    public void Configure(EntityTypeBuilder<WarehouseInventory> builder)
    {
        builder.HasKey(wi => new { wi.WarehouseId, wi.ProductId });
        builder.Property(wi => wi.ProductPrice).HasPrecision(18, 2);

        builder.HasOne<Warehouse.Domain.Model.Warehouse>()
            .WithMany()
            .HasForeignKey(wi => wi.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(wi => wi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
