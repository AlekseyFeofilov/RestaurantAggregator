using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAggregator.DAL.DbContexts;

namespace RestaurantAggregator.DAL.Extensions;

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
            b => b.MigrationsAssembly("RestaurantAggregator.API")));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); //crutch посмотреть друие способы
    }
}