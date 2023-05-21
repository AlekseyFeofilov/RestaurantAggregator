using RestaurantAggregator.Backend.API.MapperProfiles;

namespace RestaurantAggregator.Backend.API.Extensions;

public static class AddServices
{
    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DishMapperProfile));
        services.AddAutoMapper(typeof(PagedEnumerableMapperProfile));
        services.AddAutoMapper(typeof(RestaurantMapperProfile));
    }
}