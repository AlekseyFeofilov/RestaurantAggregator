using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Common.Dtos.Enums;

namespace RestaurantAggregator.Backend.Common.Dtos.Order;

public class OrderInfoDto
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

    public OrderInfoDto(Guid id, DateTime deliveryTime, DateTime orderTime, OrderStatus status, double price, string number)
    {
        Id = id;
        DeliveryTime = deliveryTime;
        OrderTime = orderTime;
        Status = status;
        Price = price;
        Number = number;
    }
}