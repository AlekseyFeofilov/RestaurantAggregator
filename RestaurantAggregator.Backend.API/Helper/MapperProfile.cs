using AutoMapper;
using RestaurantAggregator.API.Models.Dish;
using RestaurantAggregator.API.Models.Restaurant;
using RestaurantAggregator.Common.Models;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.Common.Models.Dto.Dish;
using RestaurantAggregator.Common.Models.Dto.Restaurant;

namespace RestaurantAggregator.API.Helper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<RestaurantDto, RestaurantModel>();
        CreateMap<DishDto, DishModel>();
        CreateMap<DishCreateModel, DishCreateDto>();
        CreateMap<DishModifyModel, DishModifyDto>();

        CreateMap<PagedEnumerable<DishDto>, PagedEnumerable<DishModel>>();
    }
}