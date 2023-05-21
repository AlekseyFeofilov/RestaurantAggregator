namespace RestaurantAggregator.Backend.DAL.Entities;

public class Cook
{
    public Guid Id { get; set; }
    
    public Restaurant Restaurant { get; set; }
}