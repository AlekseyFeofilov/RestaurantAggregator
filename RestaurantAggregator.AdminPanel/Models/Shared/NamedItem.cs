namespace RestaurantAggregator.AdminPanel.Models.Shared;

public class NamedItem
{
    public Guid Id { get; set; }
        
    public string Name { get; set; }

    public NamedItem(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}