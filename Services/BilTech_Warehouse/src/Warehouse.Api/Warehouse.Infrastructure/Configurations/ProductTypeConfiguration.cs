using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Domain.Model;

namespace Warehouse.Infrastructure.Configurations;

public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.HasKey(pt => pt.Id);
        builder.Property(pt => pt.Type).IsRequired().HasMaxLength(100);
        builder.Property(pt => pt.IsDeleted).HasDefaultValue(false);
    }
}
