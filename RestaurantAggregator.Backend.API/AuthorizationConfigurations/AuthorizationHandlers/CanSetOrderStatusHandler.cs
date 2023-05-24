using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Backend.API.AuthorizationConfigurations.Requirements;
using RestaurantAggregator.Backend.Common.Exceptions;
using RestaurantAggregator.Backend.DAL.DbContexts;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.IRepositories;
using RestaurantAggregator.Common.Dtos.Enums;
using RestaurantAggregator.Common.Extensions;

namespace RestaurantAggregator.Backend.API.AuthorizationConfigurations.AuthorizationHandlers;

public class CanSetOrderStatusHandler : AuthorizationHandler<CanSetOrderStatusRequirement>
{
    private readonly IHttpContextAccessor _contextAccessor;

    private readonly ApplicationDbContext _context;

    private readonly IOrderRepository _orderRepository;

    public CanSetOrderStatusHandler(IHttpContextAccessor contextAccessor, ApplicationDbContext context, IOrderRepository orderRepository)
    {
        _contextAccessor = contextAccessor;
        _context = context;
        _orderRepository = orderRepository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        CanSetOrderStatusRequirement requirement)
    {
        var user = _contextAccessor.HttpContext!.User;
        var userId = _contextAccessor.HttpContext!.User.GetNameIdentifier();

        var orderId = Guid.Parse((string)_contextAccessor.HttpContext!.GetRouteData().Values["orderId"]!);
        var order = await _orderRepository.FetchOrderAsync(orderId);

        var isSucceed = requirement.OrderStatus switch
        {
            OrderStatus.Created => false,
            OrderStatus.Kitchen =>
                user.IsInRole("Cook")
                && await GetCook(userId) is var cook && cook != null 
                && order.Restaurant.Id == cook.Restaurant.Id,
            OrderStatus.Packaging or OrderStatus.Delivering =>
                user.IsInRole("Cook")
                && await GetCook(userId) is var cook && cook != null 
                && order.Cook?.Id == cook.Id,
            OrderStatus.Delivered =>
                user.IsInRole("Courier")
                && await GetCourier(userId) is var courier && courier != null
                && order.Courier?.Id == courier.Id,
            OrderStatus.Canceled => 
                user.IsInRole("Courier") 
                && await GetCourier(userId) is var courier && courier != null
                && order.Courier?.Id == courier.Id
                || user.IsInRole("Customer") && order.UserId == userId,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (isSucceed) context.Succeed(requirement);
    }

    private Task<Cook?> GetCook(Guid userId)
    {
        return _context.Cooks
            .Include(x => x.Restaurant)
            .SingleOrDefaultAsync(x => x.Id == userId);
    }

    private Task<Courier?> GetCourier(Guid userId)
    {
        return _context.Couriers
            .Include(x => x.Restaurant)
            .SingleOrDefaultAsync(x => x.Id == userId);
    }
}