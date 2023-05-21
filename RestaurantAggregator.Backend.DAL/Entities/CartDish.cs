using System.ComponentModel.DataAnnotations;

namespace RestaurantAggregator.Backend.DAL.Entities;

public class CartDish
{
    public Guid Id { get; set; }
    
    [Range(0, int.MaxValue)]
    public int Amount { get; set; }
    
    public Guid UserId { get; set; }
    
    public Dish Dish { get; set; }
}