using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BShop.Infrastructure.Configurations;

public class ShopStorageConfiguration : IEntityTypeConfiguration<ShopStorage>
{
    public void Configure(EntityTypeBuilder<ShopStorage> builder)
    {
        builder.HasKey(x => new { x.ShopId, x.ProductId });

        builder
            .HasOne(x => x.Shop)
            .WithMany(x => x.Inventory)
            .HasForeignKey(x => x.ShopId);

        builder
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId);
    }
}