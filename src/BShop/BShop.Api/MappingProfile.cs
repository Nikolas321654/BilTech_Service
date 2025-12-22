using AutoMapper;

namespace BShop;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BShop.Domain.Model.Product, BShop.Models.ProductApi>();
        CreateMap<BShop.Domain.Model.ProductType, BShop.Models.ProductTypeApi>();
    }
}