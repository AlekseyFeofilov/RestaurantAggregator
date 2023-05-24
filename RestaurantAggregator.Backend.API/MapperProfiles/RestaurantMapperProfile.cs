using AutoMapper;
using RestaurantAggregator.Backend.API.Models.Restaurant;
using RestaurantAggregator.Backend.Common.Dtos.Restaurant;

namespace RestaurantAggregator.Backend.API.MapperProfiles;

public class RestaurantMapperProfile : Profile
{
    public RestaurantMapperProfile()
    {
        CreateMap<RestaurantDto, RestaurantModel>();
    }
}