using AutoMapper;
using RestaurantAggregator.AdminPanel.Models;
using RestaurantAggregator.AdminPanel.Models.Restaurant;
using RestaurantAggregator.Common.Models.Dto.Restaurant;

namespace RestaurantAggregator.AdminPanel.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateRestaurantModel, CreateRestaurantDto>();
        CreateMap<RestaurantDto, RestaurantModel>();
        CreateMap<ModifyRestaurantModel, ModifyRestaurantDto>();
    }
}