namespace RestaurantAggregator.AdminPanel.Models.Manager;

public class ManagerModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public Guid RestaurantId { get; set; }
}