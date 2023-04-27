using System.ComponentModel.DataAnnotations;

namespace RestaurantAggregator.Common.Models.Dto;

public class DishInCartDto
{
    public Guid Id { get; set; }
    
    [MinLength(1), Required]
    public string Name { get; set; }
    
    [Range(0, double.MaxValue), Required]
    public double Price { get; set; }
    
    public double TotalPrice => Price * Amount;
    
    [Range(0, int.MaxValue), Required]
    public int Amount { get; set; }
     
    public string? Image { get; set; }
}