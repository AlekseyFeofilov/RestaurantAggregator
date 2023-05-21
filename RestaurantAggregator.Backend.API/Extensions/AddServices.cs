using Microsoft.AspNetCore.Authorization;
using RestaurantAggregator.Backend.API.AuthorizationHandlers;
using RestaurantAggregator.Backend.API.MapperProfiles;
using RestaurantAggregator.Backend.API.Requirements;

namespace RestaurantAggregator.Backend.API.Extensions;

public static class AddServices
{
    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DishMapperProfile));
        services.AddAutoMapper(typeof(PagedEnumerableMapperProfile));
        services.AddAutoMapper(typeof(RestaurantMapperProfile));
    }

    public static void AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ModifyDish", policy =>
                policy.Requirements.Add(new CanModifyDishRequirement())
            );
            options.AddPolicy("DeleteDish", policy =>
                policy.Requirements.Add(new CanDeleteDishRequirement())
            );
            options.AddPolicy("RetrieveDish", policy =>
                policy.Requirements.Add(new CanRetrieveDishRequirement())
            );
        });
    }

    public static void AddAuthorizationHandlers(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IAuthorizationHandler, CanDeleteDishHandler>();
        services.AddScoped<IAuthorizationHandler, CanModifyDishHandler>();
        services.AddScoped<IAuthorizationHandler, CanRetrieveDishHandler>();
    }
}