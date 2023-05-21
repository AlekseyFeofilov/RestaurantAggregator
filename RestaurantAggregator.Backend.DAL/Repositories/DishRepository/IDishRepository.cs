using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.Models;
using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Backend.DAL.Repositories.DishRepository;

public interface IDishRepository
{
    Task<PagedEnumerable<Dish>> FetchAllDishesAsync(FetchDishOptions fetchDishOptions, bool isManager = false);
    
    Task<Dish> FetchDishAsync(Guid dishId, bool isManager = false);
    
    Task CreateDishAsync(Dish dish);
    
    Task ModifyDishAsync(Dish dish);
    
    Task DeleteDishAsync(Guid dishId);
}