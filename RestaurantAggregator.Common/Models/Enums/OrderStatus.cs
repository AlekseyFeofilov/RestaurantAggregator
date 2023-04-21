using System.Text.Json.Serialization;

namespace RestaurantAggregator.Common.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Created, 
    Kitchen, 
    Packaging,
    Delivery,
    Delivered,
    Canceled
}