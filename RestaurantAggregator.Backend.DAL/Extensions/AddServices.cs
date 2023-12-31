using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAggregator.Backend.DAL.DbContexts;

namespace RestaurantAggregator.Backend.DAL.Extensions;

public static class AddServices
{
    public static void AddDAL(this IServiceCollection services)
    {
        services.AddDbContext();
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
            "host=localhost;port=5432;database=backend_restaurant_aggregator_db;username=postgres;password=postgres";
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString, 
            b => b.MigrationsAssembly("RestaurantAggregator.Backend.API")));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}