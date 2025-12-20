using BShop.Domain.Model;
using BShop.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure;

public class ShopDbContext(DbContextOptions<ShopDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<ShopCheck> ShopSales { get; set; }
    public DbSet<ShopStorage> ShopStorageInventories { get; set; }
    public DbSet<WarehouseTransferRequest> WarehouseTransferRequests { get; set; }
    public DbSet<SoldProduct> SoldProducts { get; set; }
    public DbSet<WarehouseOrder> WarehouseOrders { get; set; }
    public DbSet<Worker> Workers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ShopConfiguration());
        modelBuilder.ApplyConfiguration(new WorkerConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ShopStorageConfiguration());
        modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductCheckConfiguration());
        modelBuilder.ApplyConfiguration(new SaledProductConfiguration());
        modelBuilder.ApplyConfiguration(new WarehouseOrderConfiguration());
        modelBuilder.ApplyConfiguration(new WarehouseTransferRequestConfiguration());
    }
}