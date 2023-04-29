namespace RestaurantAggregator.DAL.Entities.Staff;

public class Courier
{
    public Guid Id { get; set; }
    
    public Restaurant Restaurant { get; set; }
}