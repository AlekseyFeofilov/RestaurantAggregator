using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Backend.DAL.Entities;

public class Dish
{
    public Guid Id { get; set; }
    
    [MinLength(1), Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    [Range(0, double.MaxValue), Required]
    public double Price { get; set; }
    
    [Url]
    public string? Image { get; set; }
    
    public bool Vegetarian { get; set; }
    
    public ICollection<Review> Reviews { get; set; }
    
    [DefaultValue(0)]
    public int ReviewsAverageScore { get; set; }
    
    public DishCategory Category { get; set; }
    
    public Restaurant Restaurant { get; set; }

    public IEnumerable<Menu> Menus { get; set; }
}