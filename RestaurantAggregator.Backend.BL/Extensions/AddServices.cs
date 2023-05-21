using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAggregator.Backend.BL.Services;
using RestaurantAggregator.Backend.Common.IServices;
using RestaurantAggregator.Backend.DAL.Extensions;
using RestaurantAggregator.Backend.DAL.Repositories.CookRepository;
using RestaurantAggregator.Backend.DAL.Repositories.CourierRepository;
using RestaurantAggregator.Backend.DAL.Repositories.DishRepository;
using RestaurantAggregator.Backend.DAL.Repositories.ManagerRepository;
using RestaurantAggregator.Backend.DAL.Repositories.OrderRepository;
using RestaurantAggregator.Backend.DAL.Repositories.RestaurantRepository;
using RestaurantAggregator.Backend.DAL.Repositories.ReviewRepository;

namespace RestaurantAggregator.Backend.BL.Extensions;

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
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IDishService, DishServices>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IRepositoryService, RepositoryService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IDishRepository, DishRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IManagerRepository, ManagerRepository>();
        services.AddScoped<ICookRepository, CookRepository>();
        services.AddScoped<ICourierRepository, CourierRepository>();
        services.AddScoped<IUserService, UserService>();
    }
}