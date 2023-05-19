using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RestaurantAggregator.BL.Services;
using RestaurantAggregator.Common.IServices;
using RestaurantAggregator.DAL.Entities;
using RestaurantAggregator.DAL.Extensions;
using RestaurantAggregator.DAL.Repositories;
using RestaurantAggregator.DAL.Repositories.DishRepository;
using RestaurantAggregator.DAL.Repositories.ManagerRepository;
using RestaurantAggregator.DAL.Repositories.OrderRepository;
using RestaurantAggregator.DAL.Repositories.RestaurantRepository;
using RestaurantAggregator.DAL.Repositories.ReviewRepository;

namespace RestaurantAggregator.BL.Extensions;

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
        services.AddScoped<IUserService, UserService>();
    }
}