using System.ComponentModel.DataAnnotations;

namespace RestaurantAggregator.Backend.Common.Dtos.Order;

public class OrderCreateDto
{
    [Required]
    //[DateRange(0)]
    public DateTime DeliveryTime { get; set; }
    
    [MinLength(1), Required]
    public string Address { get; set; }

    // public OrderCreateDto(DateTime deliveryTime, string address)
    // {
    //     DeliveryTime = deliveryTime;
    //     Address = address;
    // }
}