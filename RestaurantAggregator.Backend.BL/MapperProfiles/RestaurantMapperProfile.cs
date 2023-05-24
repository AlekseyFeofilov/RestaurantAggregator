using AutoMapper;
using RestaurantAggregator.Backend.Common.Dtos.Restaurant;
using RestaurantAggregator.Backend.DAL.Entities;

namespace RestaurantAggregator.Backend.BL.MapperProfiles;

public class RestaurantMapperProfile : Profile
{
    public RestaurantMapperProfile()
    {
        CreateMap<CreateRestaurantDto, Restaurant>();
        
        CreateMap<Restaurant, RestaurantDto>();

    }
}