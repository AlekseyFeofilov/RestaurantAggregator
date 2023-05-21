using RestaurantAggregator.Backend.Common.Dto.Dish;
using RestaurantAggregator.Backend.Common.Dto.Restaurant;

namespace RestaurantAggregator.Backend.Common.Dto;

public class MenuDto
{
    public Guid Id { get; set; }
    
    public RestaurantDto Restaurant { get; set; }
    
    public IEnumerable<DishDto> Dishes { get; set; }
}