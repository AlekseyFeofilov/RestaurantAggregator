using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Backend.DAL.Entities.Staff;

public class Cook : IClassWithId
{
    public Guid Id { get; set; }
    
    public Restaurant Restaurant { get; set; }
}