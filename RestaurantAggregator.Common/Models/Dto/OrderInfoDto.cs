using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Common.Models.Dto;

public class OrderInfoDto
{
    public Guid Id { get; }
    
    [Required]
    public DateTime DeliveryTime { get; }
    
    [Required]
    public DateTime OrderTime { get; }
    
    [Required]
    public OrderStatus Status { get; }
    
    [Range(0, double.MaxValue), Required]
    public double Price { get; }

    public OrderInfoDto(Guid id, DateTime deliveryTime, DateTime orderTime, OrderStatus status, double price)
    {
        Id = id;
        DeliveryTime = deliveryTime;
        OrderTime = orderTime;
        Status = status;
        Price = price;
    }
}