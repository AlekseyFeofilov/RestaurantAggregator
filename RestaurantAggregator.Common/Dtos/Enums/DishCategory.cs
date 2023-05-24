using System.Text.Json.Serialization;

namespace RestaurantAggregator.Common.Dtos.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DishCategory
{
    Wok, 
    Pizza, 
    Soup, 
    Dessert, 
    Drink
}