using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.AdminPanel.Models.Cook;

public class CookSearchResult
{
    public string Contains { get; set; }
    
    public PagedEnumerable<CookModel> PagedRestaurants { get; set; }
}