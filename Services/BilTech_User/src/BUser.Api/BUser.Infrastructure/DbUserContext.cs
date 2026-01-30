using BUser.Domain.Model;
using BUser.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BUser.Infrastructure;

public class DbUserContext(DbContextOptions<DbUserContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}