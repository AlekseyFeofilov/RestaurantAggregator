using RestaurantAggregator.Backend.API.Models.Dish;
using RestaurantAggregator.Backend.API.Models.Restaurant;

namespace RestaurantAggregator.Backend.API.Models.Menu;

public class MenuModel
{
    public Guid Id { get; set; }
    
    public RestaurantModel Restaurant { get; set; }
    
    public IEnumerable<DishModel> Dishes { get; set; }
}