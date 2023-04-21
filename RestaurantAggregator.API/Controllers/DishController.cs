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
    [HttpGet]
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
    
     
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateDish() {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
    
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> ModifyDish() {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
    
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="401">Unauthorized</response>
    /// <response code="403">Forbidden</response>
    /// <response code="500">InternalServerError</response>
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteDish() {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}