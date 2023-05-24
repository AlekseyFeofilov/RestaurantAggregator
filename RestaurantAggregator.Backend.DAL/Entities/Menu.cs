using System.ComponentModel;
using RestaurantAggregator.Common.Dtos;

namespace RestaurantAggregator.Backend.DAL.Entities;

public class Menu : IClassWithId
{
    public Guid Id { get; set; }
    
    [DefaultValue(false)]
    public bool Active { get; set; }
    
    public Restaurant Restaurant { get; set; }
    
    public IEnumerable<Dish> Dishes { get; set; }
}