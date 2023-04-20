using RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;

namespace RestaurantAggregator.Auth.DAL.Entities.Users;

public class Courier
{
    public Guid Id { get; set; }
    
    public User User { get; set; }
}