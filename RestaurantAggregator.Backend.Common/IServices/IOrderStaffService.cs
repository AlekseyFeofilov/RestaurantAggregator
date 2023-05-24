using System.Security.Claims;
using RestaurantAggregator.Backend.Common.Dtos.Order;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IOrderStaffService
{
    Task<List<OrderInfoDto>> FetchAllCookOrdersAsync(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions);
    
    Task<List<OrderInfoDto>> FetchAllManagerOrdersAsync(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions);
    
    Task<List<OrderInfoDto>> FetchAllCourierOrdersAsync(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions);
}