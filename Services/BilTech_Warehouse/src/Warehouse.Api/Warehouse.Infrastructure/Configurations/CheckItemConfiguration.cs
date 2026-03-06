using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Model;

namespace Warehouse.Infrastructure.Configurations;

public class CheckItemConfiguration : IEntityTypeConfiguration<CheckItem>
{
    public void Configure(EntityTypeBuilder<CheckItem> builder)
    {
        builder.HasKey(ci => ci.Id);
        builder.Property(ci => ci.TotalPrice).HasPrecision(18, 2);

        builder.HasOne<WarehouseCheck>()
            .WithMany()
            .HasForeignKey(ci => ci.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
