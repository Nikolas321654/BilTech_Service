using BShop.Domain.Interfaces.Service;
using HotChocolate.Authorization;

namespace BShop.GraphQL.Mutations;

[Authorize(Roles = ["ShopWorker", "Owner"])]
[ExtendObjectType(Name = "Mutation")]
public class ProductTypeMutation
{
    public async Task<bool> RegisterProductType(
        [Service] IProductTypeService productTypeService,
        string name,
        CancellationToken cancellationToken)
    {
        await productTypeService.CreateProductType(name, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteProductType([Service] IProductTypeService productTypeService,
        Guid id,
        CancellationToken cancellationToken)
    {
        await productTypeService.DeleteProductType(id, cancellationToken);
        return true;
    }
}