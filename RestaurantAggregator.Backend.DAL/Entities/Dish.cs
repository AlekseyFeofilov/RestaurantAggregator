using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.DAL.Entities;

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
    
    public DishCategory Category { get; set; }
    
    public RestaurantEntity RestaurantEntity { get; set; }

    public IEnumerable<Menu> Menus { get; set; }
}