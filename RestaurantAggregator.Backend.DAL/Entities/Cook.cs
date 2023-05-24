using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Backend.DAL.Entities;

public class Cook : IClassWithId
{
    public Guid Id { get; set; }
    
    public Restaurant Restaurant { get; set; }
}