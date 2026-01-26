using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class ProductRepository(ShopDbContext context) : IProductRepository
{
    public async Task<Product?> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task CreateProduct(Product product, CancellationToken cancellationToken)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProduct(Product product, CancellationToken cancellationToken)
    {
        context.Products.Attach(product);
        context.Entry(product).State = EntityState.Modified;

        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (product != null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    public IQueryable<Product> GetAllProducts(CancellationToken cancellationToken)
    {
        return context.Products
            .AsNoTracking()
            .Where(x => !x.IsDeleted);
    }
}