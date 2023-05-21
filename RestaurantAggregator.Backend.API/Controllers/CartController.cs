using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Dto;
using RestaurantAggregator.Backend.Common.IServices;

namespace RestaurantAggregator.Backend.API.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    /// <summary>
    /// Get user cart
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(IEnumerable<DishInCartDto>), StatusCodes.Status200OK)]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> FetchCart()
    {
        return Ok(await _cartService.FetchCart(User));
    }

    /// <summary>
    /// Add dish to cart
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost, Route("dish/{dishId:guid}")]
    [Authorize]
    public async Task<IActionResult> AddDish(Guid dishId)
    {
        await _cartService.AddDish(User, dishId);
        return Ok();
    }

    /// <summary>
    /// Decrease the number of dishes in the cart(if increase = true), or remove the dish completely(increase = false)
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [HttpDelete, Route("dish/{dishId:guid}")]
    [Authorize]
    public async Task<IActionResult> RemoveDish(Guid dishId, bool increase = false)
    {
        await _cartService.RemoveDish(User, dishId, increase);
        return Ok();
    }
}