namespace RestaurantAggregator.AdminPanel.Models.Cook;

public class ModifyCookModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public Guid RestaurantId { get; set; }
}