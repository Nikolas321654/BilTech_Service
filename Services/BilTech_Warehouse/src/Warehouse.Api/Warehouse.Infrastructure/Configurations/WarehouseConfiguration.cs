using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Model;

namespace Warehouse.Infrastructure.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse.Domain.Model.Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse.Domain.Model.Warehouse> builder)
    {
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Address).IsRequired().HasMaxLength(500);
        builder.Property(w => w.PhoneNumber).IsRequired().HasMaxLength(20);
        builder.Property(w => w.IsDeleted).HasDefaultValue(false);
    }
}
