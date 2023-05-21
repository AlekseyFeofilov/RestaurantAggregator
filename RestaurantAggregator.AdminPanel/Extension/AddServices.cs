using RestaurantAggregator.AdminPanel.MapperProfiles;

namespace RestaurantAggregator.AdminPanel.Extension;

public static class AddServices
{
    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(RestaurantMapperProfile));
    }
}