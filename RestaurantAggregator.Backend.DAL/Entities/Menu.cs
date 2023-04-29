namespace RestaurantAggregator.DAL.Entities;

public class Menu
{
    public Guid Id { get; set; }
    
    public Restaurant Restaurant { get; set; }
    
    public IEnumerable<Dish> Dishes { get; set; }
}