using AutoMapper;
using RestaurantAggregator.AdminPanel.Models;
using RestaurantAggregator.AdminPanel.Models.Manager;
using RestaurantAggregator.AdminPanel.Models.Restaurant;
using RestaurantAggregator.Backend.Common.Dto.Restaurant;

namespace RestaurantAggregator.AdminPanel.Helpers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateRestaurantModel, CreateRestaurantDto>();
        CreateMap<RestaurantDto, RestaurantModel>();
        
        // CreateMap<ManagerDto, ManagerModel>();
        CreateMap<ModifyRestaurantModel, ModifyRestaurantDto>();
    }
}