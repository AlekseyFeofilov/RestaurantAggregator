using AutoMapper;
using RestaurantAggregator.Backend.Common.Dtos.Cart;
using RestaurantAggregator.Backend.DAL.Entities;

namespace RestaurantAggregator.Backend.BL.MapperProfiles;

public class CartMapperProfile : Profile
{
    public CartMapperProfile()
    {
        CreateMap<CartDish, DishInCartDto>()
            .ForMember(dto => dto.Name, options => options.MapFrom(dishBasket => dishBasket.Dish.Name))
            .ForMember(dto => dto.Price, options => options.MapFrom(dishBasket => dishBasket.Dish.Price))
            .ForMember(dto => dto.Image, options => options.MapFrom(dishBasket => dishBasket.Dish.Image));

    }
}