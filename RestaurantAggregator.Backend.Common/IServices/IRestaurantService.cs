using RestaurantAggregator.Common.Models.Dto;

namespace RestaurantAggregator.Common.IServices;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantDto>> FetchRestaurants(string? contains, int? page);
}