using System.Security.Claims;
using RestaurantAggregator.Backend.Common.Dtos.Order;
using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IOrderService
{
    Task<OrderDto> FetchOrder(Guid orderId);
    
    Task<PagedEnumerable<OrderInfoDto>> FetchAllOrders(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions);
    
    Task CreateOrder(ClaimsPrincipal claimsPrincipal, OrderCreateDto orderCreateDto);
    
    Task RepeatOrder(Guid orderId); 
    
    Task CancelOrder(Guid orderId);
    
    Task<IEnumerable<OrderDto>> FetchCurrentOrder(ClaimsPrincipal claimsPrincipal);
    
    Task<bool> CheckReviewAccess(ClaimsPrincipal claimsPrincipal, Guid dishId);
    
    Task SetReview(ClaimsPrincipal claimsPrincipal, Guid dishId, int rating);
}