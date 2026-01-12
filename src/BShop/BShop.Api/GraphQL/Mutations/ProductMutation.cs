using BShop.Domain.Interfaces.Service;

namespace BShop.GraphQL.Mutations;

[ExtendObjectType(Name = "Mutation")]
public class ProductMutation
{
    public async Task<bool> RegisterProduct([Service] IProductService productService,
        string name,
        decimal price,
        Guid typeId,
        CancellationToken cancellationToken)
    {
        await productService.CreateProduct(name, price, typeId, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteProduct([Service] IProductService productService,
        Guid productId,
        CancellationToken cancellationToken)
    {
        await productService.DeleteProduct(productId, cancellationToken);
        return true;
    }

    public async Task<bool> UpdateProduct([Service] IProductService productService,
        decimal price,
        Guid productId,
        CancellationToken cancellationToken)
    {
        await productService.UpdateProduct(price, productId, cancellationToken);
        return true;
    }


}