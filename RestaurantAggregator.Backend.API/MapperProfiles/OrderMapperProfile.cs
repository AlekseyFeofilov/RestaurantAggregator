using AutoMapper;
using RestaurantAggregator.Backend.API.Models.Order;
using RestaurantAggregator.Backend.Common.Dtos.Order;

namespace RestaurantAggregator.Backend.API.MapperProfiles;

public class OrderMapperProfile : Profile
{
    public OrderMapperProfile()
    {
        CreateMap<OrderCreateModel, OrderCreateDto>();
    }
}