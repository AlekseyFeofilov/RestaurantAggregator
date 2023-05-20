using RestaurantAggregator.AdminPanel.Models.Restaurant;

namespace RestaurantAggregator.AdminPanel.Models.Cook;

public class CookModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public RestaurantModel Restaurant { get; set; }
}