using AutoMapper;
using RestaurantAggregator.Backend.API.Models.Dish;
using RestaurantAggregator.Backend.API.Models.Restaurant;
using RestaurantAggregator.Backend.Common.Dtos.Dish;
using RestaurantAggregator.Backend.Common.Dtos.Restaurant;
using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Backend.API.MapperProfiles;

public class PagedEnumerableMapperProfile : Profile
{
    public PagedEnumerableMapperProfile()
    {
        CreateMap<PagedEnumerable<DishDto>, PagedEnumerable<DishModel>>();
        CreateMap<PagedEnumerable<RestaurantDto>, PagedEnumerable<RestaurantModel>>();
    }
}