using RestaurantAggregator.Common.Models.Dto.Dish;
using RestaurantAggregator.Common.Models.Dto.Restaurant;

namespace RestaurantAggregator.Common.Models.Dto;

public class MenuDto
{
    public Guid Id { get; set; }
    
    public RestaurantDto Restaurant { get; set; }
    
    public IEnumerable<DishDto> Dishes { get; set; }
}