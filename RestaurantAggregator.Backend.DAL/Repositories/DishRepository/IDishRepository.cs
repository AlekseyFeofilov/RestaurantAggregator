using RestaurantAggregator.DAL.Entities;
using RestaurantAggregator.DAL.Models;

namespace RestaurantAggregator.DAL.Repositories.DishRepository;

public interface IDishRepository
{
    Task<List<Dish>> FetchAllDishesAsync(FetchDishOptions fetchDishOptions);
    
    Task<Dish> FetchDishAsync(Guid dishId);
    
    Task CreateDishAsync(Dish dish);
    
    Task ModifyDishAsync(Dish dish);
    
    Task DeleteDishAsync(Guid dishId);
}