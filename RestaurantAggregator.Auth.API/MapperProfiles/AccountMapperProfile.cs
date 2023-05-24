using AutoMapper;
using RestaurantAggregator.Auth.API.Models.Account;
using RestaurantAggregator.Auth.Common.Dtos.Account;

namespace RestaurantAggregator.Auth.API.MapperProfiles;

public class AccountMapperProfile : Profile
{
    public AccountMapperProfile()
    {
        CreateMap<AccountDto, AccountModel>();
        CreateMap<AccountModifyModel, AccountModifyDto>();
        CreateMap<AccountRegisterModel, AccountCreateDto>();
    }
}