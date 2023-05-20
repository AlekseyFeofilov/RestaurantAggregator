namespace RestaurantAggregator.AdminPanel.Models.Courier;

public class ModifyCourierModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public Guid RestaurantId { get; set; }
}