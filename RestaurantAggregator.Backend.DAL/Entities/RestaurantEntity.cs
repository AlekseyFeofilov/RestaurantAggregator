namespace RestaurantAggregator.DAL.Entities;

public class RestaurantEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    
    public IEnumerable<Menu> Menus { get; set; }
}