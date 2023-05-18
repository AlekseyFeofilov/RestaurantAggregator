using RestaurantAggregator.Common.Models;
using RestaurantAggregator.Common.Models.Dto.Restaurant;

namespace RestaurantAggregator.AdminPanel.Models;

public class RestaurantSearchResult
{
    public string Contains { get; set; }
    
    public PagedEnumerable<RestaurantDto> PagedRestaurants { get; set; }
}