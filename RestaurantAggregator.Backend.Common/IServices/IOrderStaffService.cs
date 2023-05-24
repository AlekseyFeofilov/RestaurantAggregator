using System.Security.Claims;
using RestaurantAggregator.Backend.Common.Dtos.Order;
using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IOrderStaffService
{
    Task<PagedEnumerable<OrderInfoDto>> FetchAllCookOrdersAsync(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions);
    
    Task<PagedEnumerable<OrderInfoDto>> FetchAllManagerOrdersAsync(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions);
    
    Task<PagedEnumerable<OrderInfoDto>> FetchAllCourierOrdersAsync(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions);
}