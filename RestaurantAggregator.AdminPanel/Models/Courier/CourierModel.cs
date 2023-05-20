using RestaurantAggregator.AdminPanel.Models.Restaurant;

namespace RestaurantAggregator.AdminPanel.Models.Courier;

public class CourierModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public RestaurantModel Restaurant { get; set; }
}