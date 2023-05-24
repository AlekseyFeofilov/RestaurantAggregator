using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Backend.DAL.Entities;

public class Courier : IClassWithId
{
    public Guid Id { get; set; }
    
    public Restaurant Restaurant { get; set; }
}