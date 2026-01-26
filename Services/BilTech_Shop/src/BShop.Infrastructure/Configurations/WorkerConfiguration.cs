using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BShop.Infrastructure.Configurations;

public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder.ToTable("Workers");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.PhoneNumber).IsRequired();

        builder.HasIndex(x => x.Login).IsUnique();

        builder
            .HasMany(x => x.ManagedShops)
            .WithOne(x => x.Employee)
            .HasForeignKey(x => x.EmployeeId);
    }
}