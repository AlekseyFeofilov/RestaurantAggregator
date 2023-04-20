using System.Text.Json.Serialization;

namespace RestaurantAggregator.Common.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DishCategory
{
    Wok, 
    Pizza, 
    Soup, 
    Dessert, 
    Drink
}