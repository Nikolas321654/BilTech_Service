using AutoMapper;
using BShop.Application.Models;

namespace BShop;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Model.Product, ProductApi>();
        CreateMap<Domain.Model.ProductType, ProductTypeApi>();
        CreateMap<Domain.Model.Shop, ShopApi>();
        CreateMap<Domain.Model.SoldProduct,SoldProductApi>();
        CreateMap<Domain.Model.ShopStorage, ShopStorageApi>();
        CreateMap<Domain.Model.WarehouseOrder, WarehouseOrderApi>();
        CreateMap<Domain.Model.WarehouseTransferRequest, WarehouseTransferRequestApi>();
        CreateMap<Domain.Model.ShopCheck, ShopCheckApi>();
    }
}