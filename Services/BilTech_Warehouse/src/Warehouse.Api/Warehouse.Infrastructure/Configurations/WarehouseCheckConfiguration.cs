using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Model;

namespace Warehouse.Infrastructure.Configurations;

public class WarehouseCheckConfiguration : IEntityTypeConfiguration<WarehouseCheck>
{
    public void Configure(EntityTypeBuilder<WarehouseCheck> builder)
    {
        builder.HasKey(wc => wc.Id);
        builder.Property(wc => wc.TotalPrice).HasPrecision(18, 2);
        builder.Property(wc => wc.IsDeleted).HasDefaultValue(false);

        builder.HasOne<Warehouse.Domain.Model.Warehouse>()
            .WithMany()
            .HasForeignKey(wc => wc.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
