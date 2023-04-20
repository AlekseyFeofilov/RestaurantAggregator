using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAggregator.Auth.DAL.DbContexts;
using RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;

namespace RestaurantAggregator.Auth.DAL.Extensions;

public static class AddServices
{
    public static void AddDAL(this IServiceCollection services)
    {
        services.AddDbContext();
        services.AddIdentityService();
    }

    public static void AddAutoMigration(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        context?.Database.Migrate();
    }

    private static void AddDbContext(this IServiceCollection services)
    {
        const string connectionString =
            "host=localhost;port=5432;database=auth_restaurant_aggregator_db;username=postgres;password=postgres";
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString, 
            b => b.MigrationsAssembly("RestaurantAggregator.Auth.API")));
        // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    private static void AddIdentityService(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
}