namespace RestaurantAggregator.DAL.Entities.Staff;

public class Cook
{
    public Guid Id { get; set; }
    
    public Restaurant Restaurant { get; set; }
}