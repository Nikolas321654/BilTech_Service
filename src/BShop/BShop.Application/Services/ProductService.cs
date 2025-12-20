using BShop.Domain.Interfaces.Repository;
using BShop.Domain.Interfaces.Service;

namespace BShop.Application.Services;

public class ProductService(IProductRepository _productRepository) : IProductService
{
}