namespace RestaurantAggregator.DAL.Entities;

public class Menu
{
    public Guid Id { get; set; }
    
    public RestaurantEntity RestaurantEntity { get; set; }
    
    public IEnumerable<Dish> Dishes { get; set; }
}