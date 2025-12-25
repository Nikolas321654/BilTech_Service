using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BShop.Infrastructure.Repositories;

public class ProductRepository(ShopDbContext context) : IProductRepository
{
    public async Task<Product?> GetProductById(Guid id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<Product> CreateProduct(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task UpdateProduct(Product product)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();
    }

    public async Task DeleteProduct(Guid id)
    {
        var product = await GetProductById(id);
        context.Products.Remove(product);
        await context.SaveChangesAsync();
    }

    public IQueryable<Product> GetAllProducts()
    {
        return context.Products;
    }
}