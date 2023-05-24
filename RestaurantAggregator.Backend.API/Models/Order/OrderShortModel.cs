using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Common.Dtos.Enums;

namespace RestaurantAggregator.Backend.API.Models.Order;

public class OrderShortModel
{
    public Guid Id { get; }
    
    [Required]
    public string Number { get; set; }
    
    [Required]
    public DateTime DeliveryTime { get; }
    
    [Required]
    public DateTime OrderTime { get; }
    
    [Required]
    public OrderStatus Status { get; }
    
    [Range(0, double.MaxValue), Required]
    public double Price { get; }
}