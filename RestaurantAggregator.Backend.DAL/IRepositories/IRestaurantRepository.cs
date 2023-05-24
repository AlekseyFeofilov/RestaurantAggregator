using RestaurantAggregator.Backend.DAL.Entities;

namespace RestaurantAggregator.Backend.DAL.IRepositories;

public interface IRestaurantRepository
{
    Task<Restaurant> FetchRestaurantAsync(Guid restaurantId);
}