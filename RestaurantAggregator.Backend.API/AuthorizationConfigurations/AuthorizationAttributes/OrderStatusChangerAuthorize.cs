using Microsoft.AspNetCore.Authorization;
using RestaurantAggregator.Common.Dtos.Enums;

namespace RestaurantAggregator.Backend.API.AuthorizationConfigurations.AuthorizationAttributes;

public class OrderStatusChangerAuthorize : AuthorizeAttribute
{
    public OrderStatusChangerAuthorize(OrderStatus orderStatus)
    {
        Policy = ((int)orderStatus).ToString();
    }
}