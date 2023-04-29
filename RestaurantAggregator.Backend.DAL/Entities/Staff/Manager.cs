namespace RestaurantAggregator.DAL.Entities.Staff;

public class Manager
{
    public Guid Id { get; set; }
    
    public Restaurant Restaurant { get; set; }
}