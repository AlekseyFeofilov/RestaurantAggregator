using AutoMapper;
using RestaurantAggregator.Common.Configurations;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.Common.Models.Dto.Dish;
using RestaurantAggregator.Common.Models.Dto.Restaurant;
using RestaurantAggregator.DAL.Entities;
using RestaurantAggregator.DAL.Models;

namespace RestaurantAggregator.BL.Helper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<DishOptions, FetchDishOptions>()
            .ForMember(fetchDishOptions => fetchDishOptions.Skip, options => options.MapFrom(dishOptions => (dishOptions.Page - 1) * AppConfigurations.PageSize))
            .ForMember(fetchDishOptions => fetchDishOptions.Take, options => options.MapFrom(_ => AppConfigurations.PageSize));
        
        CreateMap<CartDish, DishInCartDto>()
            .ForMember(dto => dto.Name, options => options.MapFrom(dishBasket => dishBasket.Dish.Name))
            .ForMember(dto => dto.Price, options => options.MapFrom(dishBasket => dishBasket.Dish.Price))
            .ForMember(dto => dto.Image, options => options.MapFrom(dishBasket => dishBasket.Dish.Image));

        CreateMap<DishCreateDto, Dish>();
        CreateMap<DishModifyDto, Dish>();
        CreateMap<Dish, DishDto>();
        CreateMap<Menu, MenuDto>();
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<Order, OrderDto>();
    }
}