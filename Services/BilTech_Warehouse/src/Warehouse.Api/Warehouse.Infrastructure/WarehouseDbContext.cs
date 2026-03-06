using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Model;

namespace Warehouse.Infrastructure;

public class WarehouseDbContext : DbContext
{
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options) { }

    public DbSet<Warehouse.Domain.Model.Warehouse> Warehouses { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<WarehouseInventory> WarehouseInventories { get; set; }
    public DbSet<WarehouseCheck> WarehouseChecks { get; set; }
    public DbSet<CheckItem> CheckItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarehouseDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}