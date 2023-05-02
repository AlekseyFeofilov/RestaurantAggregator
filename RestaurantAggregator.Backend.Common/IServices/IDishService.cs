using System.Security.Claims;
using RestaurantAggregator.Common.Models;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.Common.Models.Dto.Dish;

namespace RestaurantAggregator.Common.IServices;

public interface IDishService
{
    Task<PagedEnumerable<DishDto>> FetchAllDishesAsync(DishOptions dishOptions);
    
    Task<DishDto> FetchDishAsync(Guid dishId);
    
    Task CreateDishAsync(DishCreateDto dishCreateDto);
    
    Task ModifyDishAsync(DishModifyDto dishCreateDto);
    
    Task DeleteDishAsync(Guid dishId);
}