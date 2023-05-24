using AutoMapper;
using RestaurantAggregator.Backend.API.Models.Cart;
using RestaurantAggregator.Backend.Common.Dtos.Cart;

namespace RestaurantAggregator.Backend.API.MapperProfiles;

public class CartMapperProfile : Profile
{
    public CartMapperProfile()
    {
        CreateMap<DishInCartDto, DishInCartModel>();
    }
}