using System.Security.Claims;
using RestaurantAggregator.Backend.Common.Dtos.Order;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IOrderService
{
    Task<OrderDto> FetchOrder(Guid orderId);
    
    Task<IEnumerable<OrderInfoDto>> FetchAllOrders(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions);
    
    Task CreateOrder(ClaimsPrincipal claimsPrincipal, OrderCreateDto orderCreateDto);
    
    Task RepeatOrder(Guid orderId); // todo проверять доступ с помощью Authorize 
    
    Task CancelOrder(Guid orderId);
    
    Task<IEnumerable<OrderDto>> FetchCurrentOrder(ClaimsPrincipal claimsPrincipal);
    
    Task<bool> CheckReviewAccess(ClaimsPrincipal claimsPrincipal, Guid dishId);
    
    Task SetReview(ClaimsPrincipal claimsPrincipal, Guid dishId, int rating);
}