using RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;

namespace RestaurantAggregator.Auth.DAL.Entities.Users;

public class Customer
{
    public Guid Id { get; set; }
    
    public string? Adress { get; set; }
    
    public User User { get; set; }
}