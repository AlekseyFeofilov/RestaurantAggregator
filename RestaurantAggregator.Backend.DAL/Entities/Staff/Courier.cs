using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Backend.DAL.Entities.Staff;

public class Courier : EntityWithId
{
    public Restaurant Restaurant { get; set; }
}