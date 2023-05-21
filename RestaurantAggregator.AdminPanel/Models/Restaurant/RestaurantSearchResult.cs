using RestaurantAggregator.Backend.Common.Dto.Restaurant;
using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.AdminPanel.Models.Restaurant;

public class RestaurantSearchResult
{
    public string Contains { get; set; }
    
    public PagedEnumerable<RestaurantDto> PagedRestaurants { get; set; }
}