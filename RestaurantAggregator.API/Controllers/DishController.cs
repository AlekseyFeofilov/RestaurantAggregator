using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Common.Configurations;
using RestaurantAggregator.Common.IServices;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.API.Controllers;

[ApiController]
[Route("api/dish")]
public class DishController : ControllerBase
{
    private readonly IDishService _dishService;

    public DishController(IDishService dishService)
    {
        _dishService = dishService;
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [HttpGet, Route("restaurant/{restaurantId::guid}")]
    public async Task<IActionResult> FetchAllDishes(
        Guid restaurantId,
        Guid? menuId = null,
        [FromQuery] DishCategory[]? categories = null,
        bool vegetarian = false,
        DishSorting? sorting = null,
        int page = 1)
    {
        var dishOptions = new DishOptions(restaurantId, menuId, categories, vegetarian, sorting, page);
        return Ok(await _dishService.FetchAllDishes(dishOptions));
    }

    /// <summary>
    /// Get information about concrete dish
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(DishDto), StatusCodes.Status200OK)]
    [HttpGet, Route("{dishId:guid}")]
    public async Task<IActionResult> FetchDish(Guid dishId)
    {
        return Ok(await _dishService.FetchDish(dishId));
    }

    /// <summary>
    /// Checks if user is able to set rating of the dish
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [HttpGet, Route("{dishId:guid}/rating/check")]
    [Authorize]
    public IActionResult CheckReviewAccess(Guid dishId)
    {
        return Ok(_dishService.CheckReviewAccess(User, dishId));
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
    [HttpPost, Route("{dishId:guid}/rating")]
    [Authorize]
    public async Task<IActionResult> SetReview(Guid dishId, [Range(1, 10)] int ratingScore)
    {
        await _dishService.SetReview(User, dishId, ratingScore);
        return Ok();
    }
}