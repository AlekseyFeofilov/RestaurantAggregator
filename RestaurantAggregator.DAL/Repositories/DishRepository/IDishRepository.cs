using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.DAL.Entities;
using RestaurantAggregator.DAL.Models;

namespace RestaurantAggregator.DAL.Repositories.DishRepository;

public interface IDishRepository
{
    Task<List<Dish>> FetchAllDishes(FetchDishOptions fetchDishOptions);
}