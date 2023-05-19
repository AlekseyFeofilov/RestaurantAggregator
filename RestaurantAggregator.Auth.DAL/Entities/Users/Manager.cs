using RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;
using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Auth.DAL.Entities.Users;

public class Manager : EntityWithId
{
    public User User { get; set; }
}