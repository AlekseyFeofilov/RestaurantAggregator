using System.Security.Claims;
using RestaurantAggregator.Backend.Common.Dto;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IOrderService
{
    Task<OrderDto> FetchOrder(ClaimsPrincipal claimsPrincipal, Guid orderId);
    
    Task<IEnumerable<OrderInfoDto>> FetchAllOrders(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions);
    
    Task CreateOrder(ClaimsPrincipal claimsPrincipal, OrderCreateDto orderCreateDto);
    
    Task RepeatOrder(ClaimsPrincipal claimsPrincipal, Guid orderId); // todo проверять доступ с помощью Authorize 
    
    Task CancelOrder(ClaimsPrincipal claimsPrincipal, Guid orderId);
    
    Task<IEnumerable<OrderDto>> FetchCurrentOrder(ClaimsPrincipal claimsPrincipal);
    
    Task<bool> CheckReviewAccess(ClaimsPrincipal claimsPrincipal, Guid dishId);
    
    Task SetReview(ClaimsPrincipal claimsPrincipal, Guid dishId, int rating);
}