using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Dtos.Order;
using RestaurantAggregator.Backend.Common.IServices;

namespace RestaurantAggregator.Backend.API.Controllers.StaffControllers;

[ApiController]
[Route("api")]
public class OrderStaffController : ControllerBase
{
    private readonly IOrderStaffService _orderStaffService;

    public OrderStaffController(IOrderStaffService orderStaffService) //todo заменить dto на model
    {
        _orderStaffService = orderStaffService;
    }

    /// <summary>
    /// Get a list of orders
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<OrderInfoDto>), StatusCodes.Status200OK)]
    [HttpGet, Route("cook/order")]
    [Authorize(Roles = "Cook")]
    public async Task<IActionResult> FetchAllCookOrders(
        bool current = false,
        string? numberStartWith = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        int? page = 1)
    {
        var orderOptions = new OrderOptions(current, numberStartWith, startDate, endDate, page);
        return Ok(await _orderStaffService.FetchAllCookOrdersAsync(User, orderOptions));
    }

    /// <summary>
    /// Get a list of orders
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<OrderInfoDto>), StatusCodes.Status200OK)]
    [HttpGet, Route("courier/order")]
    [Authorize(Roles = "Courier")]
    public async Task<IActionResult> FetchAllCourierOrder(
        bool current = false,
        string? numberStartWith = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        int? page = 1)
    {
        var orderOptions = new OrderOptions(current, numberStartWith, startDate, endDate, page);
        return Ok(await _orderStaffService.FetchAllCourierOrdersAsync(User, orderOptions));
    }

    /// <summary>
    /// Get a list of orders
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<OrderInfoDto>), StatusCodes.Status200OK)]
    [HttpGet, Route("manager/order")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> FetchAllManagerOrders(
        bool current = false,
        string? numberStartWith = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        int? page = 1)
    {
        var orderOptions = new OrderOptions(current, numberStartWith, startDate, endDate, page);
        return Ok(await _orderStaffService.FetchAllManagerOrdersAsync(User, orderOptions));
    }
}