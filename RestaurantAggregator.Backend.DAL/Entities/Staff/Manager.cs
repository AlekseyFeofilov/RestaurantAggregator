using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Backend.DAL.Entities.Staff;

public class Manager : EntityWithId
{
    public Guid Id { get; set; }
    
    public Restaurant Restaurant { get; set; }
}