using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAggregator.Auth.BL.Services;
using RestaurantAggregator.Auth.Common.IServices;
using RestaurantAggregator.Auth.DAL.Extensions;
using RestaurantAggregator.Auth.DAL.Repositories.CookRepository;
using RestaurantAggregator.Auth.DAL.Repositories.MangerRepository;
using RestaurantAggregator.Auth.DAL.Repositories.UserRepository;

namespace RestaurantAggregator.Auth.BL.Extensions;

public static class AddServices
{
    public static void AddBL(this IServiceCollection services)
    {
        services.AddBLServices();
        services.AddDAL();
    }

    public static void AddBLPostBuild(this WebApplication app)
    {
        app.AddAutoMigration();
    }
    
    private static void AddBLServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IManagerRepository, ManagerRepository>();
        services.AddScoped<ICookRepository, CookRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}