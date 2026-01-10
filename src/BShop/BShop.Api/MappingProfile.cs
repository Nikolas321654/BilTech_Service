using AutoMapper;

namespace BShop;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Model.Product, Models.ProductApi>();
        CreateMap<Domain.Model.ProductType, Models.ProductTypeApi>();
        CreateMap<Domain.Model.Shop, Models.ShopApi>();
        CreateMap<Domain.Model.Worker, Models.WorkerApi>();
        CreateMap<Domain.Model.SoldProduct, Models.SoldProductApi>();
        CreateMap<Domain.Model.ShopStorage, Models.ShopStorageApi>();
        CreateMap<Domain.Model.WarehouseOrder, Models.WarehouseOrderApi>();
        CreateMap<Domain.Model.WarehouseTransferRequest, Models.WarehouseTransferRequestApi>();
        CreateMap<Domain.Model.ShopCheck, Models.ShopCheckApi>();
    }
}