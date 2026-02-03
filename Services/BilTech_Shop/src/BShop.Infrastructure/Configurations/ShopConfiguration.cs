using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BShop.Infrastructure.Configurations;

public class ShopConfiguration : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.ToTable("Shops");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.PhoneNumber).IsRequired();

        builder
            .HasMany(x => x.ShopSales)
            .WithOne(x => x.Shop)
            .HasForeignKey(x => x.ShopId);

        builder
            .HasMany(x => x.Inventory)
            .WithOne(x => x.Shop)
            .HasForeignKey(x => x.ShopId);
    }
}