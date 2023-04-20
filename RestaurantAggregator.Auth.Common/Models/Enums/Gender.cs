using System.Text.Json.Serialization;

namespace RestaurantAggregator.Auth.Common.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Gender
{
    Male, 
    Female
}