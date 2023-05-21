using AutoMapper;
using RestaurantAggregator.Backend.API.Models.Dish;
using RestaurantAggregator.Backend.Common.Dto.Dish;

namespace RestaurantAggregator.Backend.API.MapperProfiles;

public class DishMapperProfile : Profile
{
    public DishMapperProfile()
    {
        CreateMap<DishDto, DishModel>();
        CreateMap<DishCreateModel, DishCreateDto>();
        CreateMap<DishModifyModel, DishModifyDto>();
    }
}