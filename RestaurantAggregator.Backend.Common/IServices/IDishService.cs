using System.Security.Claims;
using RestaurantAggregator.Backend.Common.Dtos.Dish;
using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IDishService
{
    Task<PagedEnumerable<DishDto>> FetchAllAsync(DishOptions dishOptions);
    
    Task<PagedEnumerable<DishDto>> FetchAllAsync(ClaimsPrincipal claimsPrincipal, DishOptions dishOptions, bool onlyActive = true);
    
    Task<DishDto> FetchDetailsAsync(Guid dishId, bool onlyActive = true);
    
    Task CreateAsync(ClaimsPrincipal claimsPrincipal, DishCreateDto dishCreateDto);
    
    Task<Guid> ModifyAsync(DishModifyDto dishCreateDto);
    
    Task DeleteAsync(Guid dishId);
}