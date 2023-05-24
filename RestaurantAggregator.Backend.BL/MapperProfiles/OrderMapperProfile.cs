using AutoMapper;
using RestaurantAggregator.Backend.Common.Dtos.Order;
using RestaurantAggregator.Backend.DAL.Entities;

namespace RestaurantAggregator.Backend.BL.MapperProfiles;

public class OrderMapperProfile : Profile
{
    public OrderMapperProfile()
    {
        CreateMap<Order, OrderDto>();

    }
}