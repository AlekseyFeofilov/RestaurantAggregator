using System.Security.Claims;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IStatusService
{
    Task SetKitchenStatusAsync(ClaimsPrincipal claimsPrincipal, Guid orderId);
    
    Task SetPackagingStatusAsync(Guid orderId);
    
    Task SetDeliveringStatusAsync(Guid orderId, Guid courierId);
    
    Task SetDeliveredStatusAsync(Guid orderId);
    
    Task SetCanceledStatusAsync(Guid orderId);
}