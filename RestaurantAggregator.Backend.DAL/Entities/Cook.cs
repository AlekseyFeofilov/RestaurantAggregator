namespace RestaurantAggregator.DAL.Entities;

public class Cook
{
    public Guid Id { get; set; }
    
    public RestaurantEntity RestaurantEntity { get; set; }
}