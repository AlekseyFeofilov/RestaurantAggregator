using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Backend.Common.Dto;

public class OrderDto
{
    [Required]
    public string Number { get; set; }
    
    [Required]
    public DateTime DeliveryTime { get; set; }

    [Required]
    public DateTime OrderTime { get; set; }

    [Required]
    public OrderStatus Status { get; set; }
    
    [Range(0, double.MaxValue), Required]
    public double Price { get; set; }

    [Required]
    public IEnumerable<DishInCartDto> DishBaskets { get; set; }
    
    [MinLength(1), Required]
    public string Address { get; set; }
}