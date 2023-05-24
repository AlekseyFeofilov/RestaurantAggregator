using RestaurantAggregator.Backend.Common.Dtos.Dish;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Backend.DAL.IRepositories;

public interface IDishRepository
{
    Task<PagedEnumerable<Dish>> FetchAllDishesAsync(DishOptions dishOptions, bool onlyActive = false);
    
    Task<Dish> FetchDishAsync(Guid dishId, bool onlyActive = false);
    
    Task CreateDishAsync(Dish dish);
    
    Task<Guid> ModifyDishAsync(Dish dish);
    
    Task DeleteDishAsync(Guid dishId);
}