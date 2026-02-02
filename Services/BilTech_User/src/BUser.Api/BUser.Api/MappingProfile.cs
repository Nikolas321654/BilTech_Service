using AutoMapper;
using BUser.Api.Models;
using BUser.Domain.Model;

namespace BUser.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserEntity, User>();
    }
}