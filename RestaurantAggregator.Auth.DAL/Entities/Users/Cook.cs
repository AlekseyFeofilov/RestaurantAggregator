using RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;

namespace RestaurantAggregator.Auth.DAL.Entities.Users;

public class Cook
{
    public Guid Id { get; set; }
    
    public User User { get; set; }
    
    public string ForTest { get; set; }
}