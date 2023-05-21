using AutoMapper;
using RestaurantAggregator.Backend.Common.Dto;
using RestaurantAggregator.Backend.Common.Dto.Dish;
using RestaurantAggregator.Backend.Common.Dto.Restaurant;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.Models;

namespace RestaurantAggregator.Backend.BL.Helper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CartDish, DishInCartDto>()
            .ForMember(dto => dto.Name, options => options.MapFrom(dishBasket => dishBasket.Dish.Name))
            .ForMember(dto => dto.Price, options => options.MapFrom(dishBasket => dishBasket.Dish.Price))
            .ForMember(dto => dto.Image, options => options.MapFrom(dishBasket => dishBasket.Dish.Image));

        CreateMap<CreateRestaurantDto, Restaurant>();
        CreateMap<DishOptions, FetchDishOptions>();
        CreateMap<DishCreateDto, Dish>();
        CreateMap<DishModifyDto, Dish>();
        CreateMap<Dish, DishDto>();
        CreateMap<Menu, MenuDto>();
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<Order, OrderDto>();
    }
}