using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.DAL.Entities.Staff;

public class Courier : EntityWithId
{
    public Restaurant Restaurant { get; set; }
}