using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Backend.DAL.Entities.Staff;

public class Cook : EntityWithId
{
    public Restaurant Restaurant { get; set; }
}