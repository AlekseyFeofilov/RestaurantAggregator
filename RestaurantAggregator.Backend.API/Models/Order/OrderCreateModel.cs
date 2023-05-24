using System.ComponentModel.DataAnnotations;

namespace RestaurantAggregator.Backend.API.Models.Order;

public class OrderCreateModel
{
    [Required]
    //[DateRange(0)]
    public DateTime DeliveryTime { get; set; }
    
    [MinLength(1), Required]
    public string Address { get; set; }

    // public OrderCreateModel(DateTime deliveryTime, string address)
    // {
    //     DeliveryTime = deliveryTime;
    //     Address = address;
    // }
}