using System.ComponentModel.DataAnnotations;
using RestaurantAggregator.Common.Attributes.ValidationAttributes;

namespace RestaurantAggregator.Backend.API.Models.Order;

public class OrderCreateModel
{
    [Required]
    [DateRange(earlierThanTodayBy: -0.5, isNullable: true)]
    public DateTime DeliveryTime { get; set; }
    
    [MinLength(1), Required]
    public string Address { get; set; }

    // public OrderCreateModel(DateTime deliveryTime, string address)
    // {
    //     DeliveryTime = deliveryTime;
    //     Address = address;
    // }
}