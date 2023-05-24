using RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;
using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Auth.DAL.Entities.Users;

public class Cook : IClassWithId
{
    public Guid Id { get; set; }
    
    public User User { get; set; }
}