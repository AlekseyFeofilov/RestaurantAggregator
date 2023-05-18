using AutoMapper;
using RestaurantAggregator.Backend.API.Models.Dish;
using RestaurantAggregator.Backend.API.Models.Restaurant;
using RestaurantAggregator.Common.Models;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.Common.Models.Dto.Dish;
using RestaurantAggregator.Common.Models.Dto.Restaurant;

namespace RestaurantAggregator.Backend.API.Helper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RestaurantDto, RestaurantModel>();
        CreateMap<DishDto, DishModel>();
        CreateMap<DishCreateModel, DishCreateDto>();
        CreateMap<DishModifyModel, DishModifyDto>();

        CreateMap<PagedEnumerable<DishDto>, PagedEnumerable<DishModel>>();
        CreateMap<PagedEnumerable<RestaurantDto>, PagedEnumerable<RestaurantModel>>();
    }
}