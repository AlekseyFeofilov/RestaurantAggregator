using RestaurantAggregator.Backend.Common.Dto;
using RestaurantAggregator.Backend.Common.Dto.Dish;
using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IDishService
{
    Task<PagedEnumerable<DishDto>> FetchAllDishesAsync(DishOptions dishOptions);
    
    Task<DishDto> FetchDishAsync(Guid dishId);
    
    Task CreateDishAsync(DishCreateDto dishCreateDto);
    
    Task ModifyDishAsync(DishModifyDto dishCreateDto);
    
    Task DeleteDishAsync(Guid dishId);
}