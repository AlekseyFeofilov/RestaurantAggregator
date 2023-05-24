using System.Security.Claims;
using RestaurantAggregator.Backend.Common.IServices;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.Repositories.CookRepository;
using RestaurantAggregator.Backend.DAL.Repositories.CourierRepository;
using RestaurantAggregator.Backend.DAL.Repositories.OrderRepository;
using RestaurantAggregator.Common.Extensions;
using RestaurantAggregator.Common.Models;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Backend.BL.Services;

public class StatusService : IStatusService
{
    private readonly IOrderRepository _orderRepository;

    private readonly ICookRepository _cookRepository;

    private readonly ICourierRepository _courierRepository;

    private readonly IStatusChangingMessageService _statusChangingMessageService;

    public StatusService(IOrderRepository orderRepository, ICookRepository cookRepository, ICourierRepository courierRepository, IStatusChangingMessageService statusChangingMessageService)
    {
        _orderRepository = orderRepository;
        _cookRepository = cookRepository;
        _courierRepository = courierRepository;
        _statusChangingMessageService = statusChangingMessageService;
    }

    public async Task SetKitchenStatusAsync(ClaimsPrincipal claimsPrincipal, Guid orderId)
    {
        await SetOrderStatus(orderId, order =>
        {
            order.Status = OrderStatus.Kitchen;
            order.Cook = _cookRepository.FetchDetails(claimsPrincipal.GetNameIdentifier());
        });
    }

    public async Task SetPackagingStatusAsync(Guid orderId)
    {
        await SetOrderStatus(orderId, order => order.Status = OrderStatus.Packaging);
    }

    public async Task SetDeliveringStatusAsync(Guid orderId, Guid courierId)
    {
        await SetOrderStatus(orderId, order =>
        {
            order.Status = OrderStatus.Delivering;
            order.Courier = _courierRepository.FetchDetails(courierId);
        });
    }

    public async Task SetDeliveredStatusAsync(Guid orderId)
    {
        await SetOrderStatus(orderId, order => order.Status = OrderStatus.Delivered);
    }

    public async Task SetCanceledStatusAsync(Guid orderId)
    {
        await SetOrderStatus(orderId, order => order.Status = OrderStatus.Canceled);
    }

    private async Task SetOrderStatus(Guid orderId, Action<Order> exp)
    {
        var order = await _orderRepository.FetchOrderAsync(orderId);
        exp(order);
        await _orderRepository.SaveChangesAsync();
        _statusChangingMessageService.SendStatusChangingMessage(new StatusChangingNotification(order.UserId, order.Number, order.Status));
    }
}