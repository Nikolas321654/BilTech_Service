using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BShop.Infrastructure.Configurations;

public class WarehouseTransferRequestConfiguration : IEntityTypeConfiguration<WarehouseTransferRequest>
{
    public void Configure(EntityTypeBuilder<WarehouseTransferRequest> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(x => x.Shop)
            .WithMany(x => x.WarehouseTransferRequests)
            .HasForeignKey(x => x.ShopId);

        builder.HasIndex(x => x.ShopId);
    }
}