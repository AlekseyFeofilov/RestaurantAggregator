using AutoMapper;
using RestaurantAggregator.Auth.Common.Dtos.Account;
using RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;

namespace RestaurantAggregator.Auth.BL.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<AccountCreateDto, User>();
        CreateMap<User, AccountDto>();
    }
}