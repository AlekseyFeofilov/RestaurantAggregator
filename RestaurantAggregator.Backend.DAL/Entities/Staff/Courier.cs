using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Backend.DAL.Entities.Staff;

public class Courier : IClassWithId
{
    public Guid Id { get; set; }
    
    public Restaurant Restaurant { get; set; }
}