using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RestaurantAggregator.Common.Models.Dto;

public class OrderCreateDto
{
    [Required]
    //[DateRange(0)]
    public DateTime DeliveryTime { get; }
    
    [MinLength(1), Required]
    public string Address { get; }

    public OrderCreateDto(DateTime deliveryTime, string address)
    {
        DeliveryTime = deliveryTime;
        Address = address;
    }
}