using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Backend.API.AuthorizationConfigurations.AuthorizationAttributes;
using RestaurantAggregator.Backend.Common.IServices;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Backend.API.Controllers.StaffControllers;

[ApiController]
[Route("api/order/{orderId:guid}/status")]
public class StatusController : ControllerBase
{
    private readonly IStatusService _statusService;

    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Not Authorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost, Route("kitchen")]
    [OrderStatusChangerAuthorize(OrderStatus.Kitchen)]
    public async Task<IActionResult> SetKitchenStatus(Guid orderId)
    {
        await _statusService.SetKitchenStatusAsync(User, orderId);
        return Ok();
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Not Authorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost, Route("packaging")]
    [OrderStatusChangerAuthorize(OrderStatus.Packaging)]
    public async Task<IActionResult> SetPackagingStatus(Guid orderId)
    {
        await _statusService.SetPackagingStatusAsync(orderId);
        return Ok();
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Not Authorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost, Route("delivering")]
    [OrderStatusChangerAuthorize(OrderStatus.Delivering)]
    public async Task<IActionResult> SetDeliveringStatus(Guid orderId, Guid courierId)
    {
        await _statusService.SetDeliveringStatusAsync(orderId, courierId);
        return Ok();
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Not Authorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost, Route("delivered")]
    [OrderStatusChangerAuthorize(OrderStatus.Delivered)]
    public async Task<IActionResult> SetDeliveredStatus(Guid orderId)
    {
        await _statusService.SetDeliveredStatusAsync(orderId);
        return Ok();
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Not Authorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost, Route("canceled")]
    [OrderStatusChangerAuthorize(OrderStatus.Canceled)]
    public async Task<IActionResult> SetCanceledStatus(Guid orderId)
    {
        await _statusService.SetCanceledStatusAsync(orderId);
        return Ok();
    }
}