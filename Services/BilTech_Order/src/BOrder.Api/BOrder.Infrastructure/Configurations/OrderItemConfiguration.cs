using BOrder.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BOrder.Infrastructure.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
    {
        builder.HasKey(x => new { x.OrderId, x.ProductId });
        builder.Property(x => x.Quantity).IsRequired();
    }
}