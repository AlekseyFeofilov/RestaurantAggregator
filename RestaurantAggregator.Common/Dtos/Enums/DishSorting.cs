using System.Text.Json.Serialization;

namespace RestaurantAggregator.Common.Dtos.Enums;

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