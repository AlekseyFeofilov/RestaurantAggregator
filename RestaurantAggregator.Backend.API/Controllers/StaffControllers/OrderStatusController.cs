using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Backend.Common.IServices;

namespace RestaurantAggregator.Backend.API.Controllers.StaffControllers;

[ApiController]
[Route("api")]
public class OrderStatusController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderStatusController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="500">InternalServerError</response>
    [HttpGet, Route("status")]
    public async Task<IActionResult> FetchOrderStatuses() {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <response code="200">Success</response>
    /// <response code="401">Not Authorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [HttpPatch, Route("customer/order/{orderId::guid}/status")]
    public async Task<IActionResult> ChangeOrderStatusByCustomer(Guid orderId, [FromBody] Guid statusId) {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
    
    /// <response code="200">Success</response>
    /// <response code="401">Not Authorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [HttpPatch, Route("cook/order/{orderId::guid}/status")]
    public async Task<IActionResult> ChangeOrderStatusByCook(Guid orderId, [FromBody] Guid statusId) {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <response code="200">Success</response>
    /// <response code="401">Not Authorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [HttpPatch, Route("courier/order/{orderId::guid}/status")]
    public async Task<IActionResult> ChangeOrderStatusByCourier(Guid orderId, [FromBody] Guid statusId) {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <response code="200">Success</response>
    /// <response code="401">Not Authorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [HttpPatch, Route("manager/order/{orderId::guid}/status")]
    public async Task<IActionResult> ChangeOrderStatusByManager(Guid orderId, [FromBody] Guid statusId) {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}