using System.Security.Claims;
using RestaurantAggregator.Common.Models.Dto;

namespace RestaurantAggregator.Common.IServices;

public interface IDishService
{
    Task<DishPagedListDto> FetchAllDishes(DishOptions dishOptions);
    
    Task<DishDto> FetchDish(Guid dishId);
    
    bool CheckReviewAccess(ClaimsPrincipal claimsPrincipal, Guid dishId);
    
    Task SetReview(ClaimsPrincipal claimsPrincipal, Guid dishId, int rating);
}