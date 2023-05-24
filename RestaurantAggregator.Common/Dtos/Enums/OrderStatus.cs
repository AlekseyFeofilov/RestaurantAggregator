using System.Text.Json.Serialization;

namespace RestaurantAggregator.Common.Dtos.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Created, 
    Kitchen, 
    Packaging,
    Delivering,
    Delivered,
    Canceled
}