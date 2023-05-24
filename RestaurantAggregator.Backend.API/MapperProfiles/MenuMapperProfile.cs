using AutoMapper;
using RestaurantAggregator.Backend.API.Models.Menu;
using RestaurantAggregator.Backend.Common.Dtos.Menu;

namespace RestaurantAggregator.Backend.API.MapperProfiles;

public class MenuMapperProfile : Profile
{
    public MenuMapperProfile()
    {
        CreateMap<MenuDto, MenuModel>();
    }
}