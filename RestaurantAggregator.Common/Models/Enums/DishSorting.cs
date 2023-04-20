using System.Text.Json.Serialization;

namespace RestaurantAggregator.Common.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DishSorting
{
    NameAsc, 
    NameDesc, 
    PriceAsc, 
    PriceDesc, 
    RatingAsc, 
    RatingDesc
}