using RestaurantAggregator.AdminPanel.Models.Restaurant;

namespace RestaurantAggregator.AdminPanel.Models.Manager;

public class ManagerModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public RestaurantModel Restaurant { get; set; }
}