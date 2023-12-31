using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Backend.API.Models.Order;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Dtos.Order;
using RestaurantAggregator.Backend.Common.IServices;

namespace RestaurantAggregator.Backend.API.Controllers.CustomerControllers;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase //todo заменить dto на model
{
    private readonly IOrderService _orderService;

    private readonly IMapper _mapper;

    public OrderController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    /// <summary>
    /// Get information about concrete order
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [HttpGet, Route("{orderId:guid}")]
    [Authorize]
    public async Task<IActionResult> FetchOrder(Guid orderId)
    {
        return Ok(await _orderService.FetchOrder(orderId));
    }
    
    /// <summary>
    /// Get a list of orders
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<OrderInfoDto>), StatusCodes.Status200OK)]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> FetchAllOrdersByCustomer(
        bool current = false,
        string? numberStartWith = null,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        int page = 1)
    {
        var orderOptions = new OrderOptions(current, numberStartWith, startDate, endDate, page);
        return Ok(await _orderService.FetchAllOrders(User, orderOptions));
    }
    
    /// <summary>
    /// Get information about concrete order
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [HttpGet, Route("current")]
    [Authorize]
    public async Task<IActionResult> FetchCurrentOrder()
    {
        return Ok(await _orderService.FetchCurrentOrder(User));
    }
    
    /// <summary>
    /// Creating the order from dishes in basket
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreateModel orderCreateModel)
    {
        
        await _orderService.CreateOrder(User, _mapper.Map<OrderCreateDto>(orderCreateModel));
        return Ok();
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost, Route("{orderId::guid}")]
    [Authorize]
    public async Task<IActionResult> RepeatOrder(Guid orderId)
    {
        await _orderService.RepeatOrder(orderId);
        return Ok();
    }
    
    

    /// <summary>
    /// Set a rating for a dish
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost, Route("{orderId:guid}/dish/{dishId:guid}/rating")]
    [Authorize]
    public async Task<IActionResult> SetReview(Guid orderId, Guid dishId, [Range(1, 10)] int ratingScore)
    {
        // await _dishService.SetReview(User, dishId, ratingScore);
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}