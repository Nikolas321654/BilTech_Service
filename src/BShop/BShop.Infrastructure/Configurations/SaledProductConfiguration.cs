using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BShop.Infrastructure.Configurations;

public class SaledProductConfiguration : IEntityTypeConfiguration<SoldProduct>
{
    public void Configure(EntityTypeBuilder<SoldProduct> builder)
    {
        builder.HasKey(x => new { x.OrderId, x.ProductId });

        builder
            .HasOne(x => x.ShopCheck)
            .WithMany(x => x.SaledProducts)
            .HasForeignKey(x => x.OrderId);

        builder
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId);
    }
}