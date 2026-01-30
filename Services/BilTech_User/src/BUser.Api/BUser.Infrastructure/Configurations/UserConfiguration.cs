using BUser.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BUser.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Login).IsUnique();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.Role).IsRequired();
        builder.Property(x => x.WorkPlaceId).IsRequired();
    }
}