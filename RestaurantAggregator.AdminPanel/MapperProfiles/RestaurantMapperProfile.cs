using AutoMapper;
using RestaurantAggregator.AdminPanel.Models.Restaurant;
using RestaurantAggregator.Backend.Common.Dtos.Restaurant;

namespace RestaurantAggregator.AdminPanel.MapperProfiles;

public class RestaurantMapperProfile : Profile
{
    public RestaurantMapperProfile()
    {
        CreateMap<CreateRestaurantModel, CreateRestaurantDto>();
        CreateMap<RestaurantDto, RestaurantModel>();
        
        // CreateMap<ManagerDto, ManagerModel>();
        CreateMap<ModifyRestaurantModel, ModifyRestaurantDto>();
    }
}