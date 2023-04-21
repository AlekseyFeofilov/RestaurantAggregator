namespace RestaurantAggregator.Common.Models.Dto;

public class MenuDto
{
    public Guid Id { get; set; }
    
    public RestaurantDto Restaurant { get; set; }
    
    public IEnumerable<DishDto> Dishes { get; set; }
}