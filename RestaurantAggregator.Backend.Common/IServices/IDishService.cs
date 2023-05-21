using RestaurantAggregator.Backend.Common.Dto;
using RestaurantAggregator.Backend.Common.Dto.Dish;
using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Backend.Common.IServices;

public interface IDishService
{
    Task<PagedEnumerable<DishDto>> FetchAllAsync(DishOptions dishOptions);
    
    Task<DishDto> FetchDetailsAsync(Guid dishId);
    
    Task CreateAsync(DishCreateDto dishCreateDto);
    
    Task ModifyAsync(DishModifyDto dishCreateDto);
    
    Task DeleteAsync(Guid dishId);
}