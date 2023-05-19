using RestaurantAggregator.DAL.Entities;

namespace RestaurantAggregator.DAL.Repositories.RestaurantRepository;

public interface IRestaurantRepository
{
    Task<Restaurant> FetchRestaurantAsync(Guid restaurantId);
}