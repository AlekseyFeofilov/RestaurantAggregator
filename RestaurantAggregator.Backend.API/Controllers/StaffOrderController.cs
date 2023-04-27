using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Common.Configurations;
using RestaurantAggregator.Common.IServices;
using RestaurantAggregator.Common.Models.Dto;

namespace RestaurantAggregator.API.Controllers;

[ApiController]
[Route("api")]
public class StaffOrderController : ControllerBase
{
    private readonly IOrderService _orderService;


    public StaffOrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Get a list of orders
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(IEnumerable<OrderInfoDto>), StatusCodes.Status200OK)]
    [HttpGet, Route("cook/order")]
    [Authorize]
    public async Task<IActionResult> FetchAllOrdersByCook(
        bool current = false,
        int? numberStartWith = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        int? page = 1)
    {
        var orderOptions = new OrderOptions(current, numberStartWith, startDate, endDate, page);
        return Ok(await _orderService.FetchAllOrders(User, orderOptions));
    }

    /// <summary>
    /// Get a list of orders
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(IEnumerable<OrderInfoDto>), StatusCodes.Status200OK)]
    [HttpGet, Route("cook/order/history")]
    [Authorize]
    public async Task<IActionResult> FetchAllCookOrderHistory(
        bool current = false,
        int? numberStartWith = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        int? page = 1)
    {
        var orderOptions = new OrderOptions(current, numberStartWith, startDate, endDate, page);
        return Ok(await _orderService.FetchAllOrders(User, orderOptions));
    }

    /// <summary>
    /// Get a list of orders
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(IEnumerable<OrderInfoDto>), StatusCodes.Status200OK)]
    [HttpGet, Route("courier/order")]
    [Authorize]
    public async Task<IActionResult> FetchAllOrdersByCourier(
        bool current = false,
        int? numberStartWith = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        int? page = 1)
    {
        var orderOptions = new OrderOptions(current, numberStartWith, startDate, endDate, page);
        return Ok(await _orderService.FetchAllOrders(User, orderOptions));
    }

    /// <summary>
    /// Get a list of orders
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(IEnumerable<OrderInfoDto>), StatusCodes.Status200OK)]
    [HttpGet, Route("courier/order/history")]
    [Authorize]
    public async Task<IActionResult> FetchAllCourierOrderHistory(
        bool current = false,
        int? numberStartWith = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        int? page = 1)
    {
        var orderOptions = new OrderOptions(current, numberStartWith, startDate, endDate, page);
        return Ok(await _orderService.FetchAllOrders(User, orderOptions));
    }

    /// <summary>
    /// Get a list of orders
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(IEnumerable<OrderInfoDto>), StatusCodes.Status200OK)]
    [HttpGet, Route("manager/order")]
    [Authorize]
    public async Task<IActionResult> FetchAllOrdersByManager(
        bool current = false,
        int? numberStartWith = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        int? page = 1)
    {
        var orderOptions = new OrderOptions(current, numberStartWith, startDate, endDate, page);
        return Ok(await _orderService.FetchAllOrders(User, orderOptions));
    }

    /// <summary>
    /// Get a list of orders
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(IEnumerable<OrderInfoDto>), StatusCodes.Status200OK)]
    [HttpGet, Route("manager/order/history")]
    [Authorize]
    public async Task<IActionResult> FetchAllManagerOrderHistory(
        bool current = false,
        int? numberStartWith = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        int? page = 1)
    {
        var orderOptions = new OrderOptions(current, numberStartWith, startDate, endDate, page);
        return Ok(await _orderService.FetchAllOrders(User, orderOptions));
    }
}