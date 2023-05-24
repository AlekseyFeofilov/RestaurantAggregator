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
                && order.Restaurant.Id == GetCook(userId).Restaurant.Id,
            OrderStatus.Packaging or OrderStatus.Delivering =>
                user.IsInRole("Cook")
                && order.Cook?.Id == GetCook(userId).Id, // todo сделать какую-то кастомную ошибку о том, что данный статус нельзя поставить пока не будет поставлен другой
            OrderStatus.Delivered =>
                user.IsInRole("Courier")
                && order.Courier?.Id == GetCourier(userId).Id,
            OrderStatus.Canceled => 
                user.IsInRole("Courier") && order.Courier?.Id == GetCourier(userId).Id
                || user.IsInRole("Customer") && order.UserId == userId,
            _ => throw new ArgumentOutOfRangeException()
        };

        if (isSucceed) context.Succeed(requirement);
    }

    private Cook GetCook(Guid userId)
    {
        var cook = _context.Cooks
            .Include(x => x.Restaurant)
            .SingleOrDefault(x => x.Id == userId);

        if (cook == null)
        {
            throw new InvalidTokenException();
        }

        return cook;
    }

    private Courier GetCourier(Guid userId)
    {
        var courier = _context.Couriers
            .Include(x => x.Restaurant)
            .SingleOrDefault(x => x.Id == userId);

        if (courier == null)
        {
            throw new InvalidTokenException();
        }

        return courier;
    }
}