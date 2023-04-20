using System.Security.Claims;
using RestaurantAggregator.Common.Models.Dto;

namespace RestaurantAggregator.Common.IServices;

public interface ICartService
{
    Task<IEnumerable<DishInCartDto>> FetchCart(ClaimsPrincipal claimsPrincipal);
    Task AddDish(ClaimsPrincipal claimsPrincipal, Guid dishId);
    Task RemoveDish(ClaimsPrincipal claimsPrincipal, Guid dishId, bool increase = false);
}