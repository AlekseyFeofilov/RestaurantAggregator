using RestaurantAggregator.Backend.Common.Dtos.Dish;
using RestaurantAggregator.Backend.Common.Dtos.Restaurant;

namespace RestaurantAggregator.Backend.Common.Dtos.Menu;

public class MenuDto
{
    public Guid Id { get; set; }
    
    public RestaurantDto Restaurant { get; set; }
    
    public IEnumerable<DishDto> Dishes { get; set; }
}