using Warehouse.Domain.Interfaces.Repositories;
using Warehouse.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Warehouse.Infrastructure.Repositories;

public class ProductRepository(WarehouseDbContext dbContext) : IProductRepository
{
    public async Task<Product?> GetProductById(Guid productId, CancellationToken cancellationToken)
    {
        return await dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == productId && !p.IsDeleted, cancellationToken);
    }

    public IQueryable<Product> GetAllProducts(CancellationToken cancellationToken)
    {
        return dbContext.Products.Where(p => !p.IsDeleted);
    }

    public async Task CreateProduct(Product product, CancellationToken cancellationToken)
    {
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProduct(Product product, CancellationToken cancellationToken)
    {
        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteProduct(Guid productId, CancellationToken cancellationToken)
    {
        var product = await GetProductById(productId, cancellationToken);
        if (product != null)
        {
            product.IsDeleted = true;
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}