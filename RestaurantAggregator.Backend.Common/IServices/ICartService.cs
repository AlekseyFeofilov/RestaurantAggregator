using System.Security.Claims;
using RestaurantAggregator.Backend.Common.Dtos.Cart;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface ICartService
{
    Task<IEnumerable<DishInCartDto>> FetchCart(ClaimsPrincipal claimsPrincipal);
    
    Task AddDish(ClaimsPrincipal claimsPrincipal, Guid dishId);
    
    Task RemoveDish(ClaimsPrincipal claimsPrincipal, Guid dishId, bool increase = false);
}