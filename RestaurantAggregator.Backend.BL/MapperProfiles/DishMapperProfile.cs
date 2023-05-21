using AutoMapper;
using RestaurantAggregator.Backend.Common.Dto;
using RestaurantAggregator.Backend.Common.Dto.Dish;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.Models;

namespace RestaurantAggregator.Backend.BL.MapperProfiles;

public class DishMapperProfile : Profile
{
    public DishMapperProfile()
    {
        CreateMap<DishOptions, FetchDishOptions>();
        CreateMap<DishCreateDto, Dish>();
        CreateMap<DishModifyDto, Dish>();
        CreateMap<Dish, DishDto>();
    }
}