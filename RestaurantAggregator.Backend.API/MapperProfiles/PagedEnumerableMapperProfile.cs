using AutoMapper;
using RestaurantAggregator.Backend.API.Models.Dish;
using RestaurantAggregator.Backend.API.Models.Restaurant;
using RestaurantAggregator.Backend.Common.Dto.Dish;
using RestaurantAggregator.Backend.Common.Dto.Restaurant;
using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Backend.API.MapperProfiles;

public class PagedEnumerableMapperProfile : Profile
{
    public PagedEnumerableMapperProfile()
    {
        CreateMap<PagedEnumerable<DishDto>, PagedEnumerable<DishModel>>();
        CreateMap<PagedEnumerable<RestaurantDto>, PagedEnumerable<RestaurantModel>>();
    }
}