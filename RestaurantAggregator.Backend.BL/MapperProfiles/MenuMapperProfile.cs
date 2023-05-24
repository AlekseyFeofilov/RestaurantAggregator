using AutoMapper;
using RestaurantAggregator.Backend.Common.Dtos.Menu;
using RestaurantAggregator.Backend.DAL.Entities;

namespace RestaurantAggregator.Backend.BL.MapperProfiles;

public class MenuMapperProfile : Profile
{
    public MenuMapperProfile()
    {
        CreateMap<Menu, MenuDto>();

    }
}