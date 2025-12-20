using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BShop.Infrastructure.Configurations;

public class ProductCheckConfiguration : IEntityTypeConfiguration<ShopCheck>
{
    public void Configure(EntityTypeBuilder<ShopCheck> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .HasMany(x => x.SaledProducts)
            .WithOne(x => x.ShopCheck)
            .HasForeignKey(x => x.OrderId);
    }
}