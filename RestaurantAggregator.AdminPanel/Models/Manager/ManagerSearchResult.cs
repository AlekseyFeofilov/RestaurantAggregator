using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.AdminPanel.Models.Manager;

public class ManagerSearchResult
{
    public string Contains { get; set; }
    
    public PagedEnumerable<ManagerModel> PagedRestaurants { get; set; }
}