using System.Security.Claims;
using RestaurantAggregator.Common.Models.Dto;

namespace RestaurantAggregator.Common.IServices;

public interface IOrderService
{
    Task<OrderDto> FetchOrder(ClaimsPrincipal claimsPrincipal, Guid orderId);
    Task<IEnumerable<OrderInfoDto>> FetchAllOrders(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions);
    Task CreateOrder(ClaimsPrincipal claimsPrincipal, OrderCreateDto orderCreateDto);
    Task RepeatOrder(ClaimsPrincipal claimsPrincipal, Guid orderId); // todo проверять доступ с помощью Authorize 
    Task CancelOrder(ClaimsPrincipal claimsPrincipal, Guid orderId);
    Task<IEnumerable<OrderDto>> FetchCurrentOrder(ClaimsPrincipal claimsPrincipal);
}