using RestaurantAggregator.Backend.DAL.Entities;

namespace RestaurantAggregator.Backend.DAL.Repositories.RestaurantRepository;

public interface IRestaurantRepository
{
    Task<Restaurant> FetchRestaurantAsync(Guid restaurantId);
}