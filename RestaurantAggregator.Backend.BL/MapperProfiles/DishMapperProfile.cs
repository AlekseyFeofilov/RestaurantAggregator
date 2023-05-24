using AutoMapper;
using RestaurantAggregator.Backend.Common.Dtos.Dish;
using RestaurantAggregator.Backend.DAL.Entities;

namespace RestaurantAggregator.Backend.BL.MapperProfiles;

public class DishMapperProfile : Profile
{
    public DishMapperProfile()
    {
        CreateMap<DishCreateDto, Dish>();
        CreateMap<DishModifyDto, Dish>();
        CreateMap<Dish, DishDto>();
    }
}