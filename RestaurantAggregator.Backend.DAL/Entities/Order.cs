using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Common.Models.Enums;

#pragma warning disable CS8618

namespace RestaurantAggregator.Backend.DAL.Entities;

public class Order
{
    public Guid Id { get; set; }
    
    [Required] 
    public DateTime DeliveryTime { get; set; }

    [Required]
    public DateTime OrderTime { get; set; }

    [Required] 
    public OrderStatus Status { get; set; }
    
    [Range(0, double.MaxValue), Required] 
    public double Price { get; set; }
    
    [Required] 
    public ICollection<CartDish> DishBaskets { get; set; }
    
    [MinLength(1), Required]
    public string Address { get; set; }
    
    public Guid UserId { get; set; }
}