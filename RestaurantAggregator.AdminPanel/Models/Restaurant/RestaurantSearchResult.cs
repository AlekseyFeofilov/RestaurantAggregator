using RestaurantAggregator.Backend.Common.Dtos.Restaurant;
using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.AdminPanel.Models.Restaurant;

public class RestaurantSearchResult
{
    public string Contains { get; set; }
    
    public PagedEnumerable<RestaurantDto> PagedRestaurants { get; set; }
}