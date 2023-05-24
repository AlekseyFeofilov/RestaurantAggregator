using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Backend.DAL.Entities;

public class Manager : IClassWithId
{
    public Guid Id { get; set; }
    
    public Restaurant Restaurant { get; set; }
}