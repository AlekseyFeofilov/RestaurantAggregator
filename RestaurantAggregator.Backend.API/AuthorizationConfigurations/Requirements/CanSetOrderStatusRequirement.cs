using Microsoft.AspNetCore.Authorization;
using RestaurantAggregator.Common.Dtos.Enums;

namespace RestaurantAggregator.Backend.API.AuthorizationConfigurations.Requirements;

public class CanSetOrderStatusRequirement : IAuthorizationRequirement
{
    public OrderStatus OrderStatus;

    public CanSetOrderStatusRequirement(OrderStatus orderStatus)
    {
        OrderStatus = orderStatus;
    }
}