using RestaurantAggregator.AdminPanel.Models.Manager;
using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.AdminPanel.Models.Courier;

public class SearchCourierResult
{
    public string Contains { get; set; }
    
    public PagedEnumerable<ManagerModel> PagedRestaurants { get; set; }
}