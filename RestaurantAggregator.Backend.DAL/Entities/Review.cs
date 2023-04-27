using System.ComponentModel.DataAnnotations;

namespace RestaurantAggregator.DAL.Entities;

public class Review
{
    public Guid Id { get; set; }
    
    [Required]
    public Dish Dish { get; set; }
    
    public Guid UserId { get; set; }
    
    [Range(1, 10)]
    public int Rating { get; set; }
}