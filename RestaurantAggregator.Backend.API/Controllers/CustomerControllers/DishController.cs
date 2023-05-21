using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantAggregator.Backend.API.Models.Dish;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Dto;
using RestaurantAggregator.Backend.Common.IServices;
using RestaurantAggregator.Common.Models;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Backend.API.Controllers.CustomerControllers;

[ApiController]
[Route("api/dish")] // todo make with configuration
public class DishController : ControllerBase
{
    private readonly IDishService _dishService;

    private readonly IMapper _mapper;

    public DishController(IDishService dishService, IMapper mapper)
    {
        _dishService = dishService;
        _mapper = mapper;
    }

    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(IEnumerable<DishModel>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> FetchAllDishes(
        [Required] Guid restaurantId,
        Guid? menuId = null,
        [FromQuery] DishCategory[]? categories = null,
        bool vegetarian = false,
        DishSorting? sorting = null,
        int page = 1)
    {
        var dishOptions = new DishOptions(restaurantId, menuId, categories, vegetarian, sorting, page);
        var dishPagedListDto = await _dishService.FetchAllAsync(dishOptions);
        
        return Ok(_mapper.Map<PagedEnumerable<DishModel>>(dishPagedListDto));
    }

    /// <summary>
    /// Get information about concrete dish
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">InternalServerError</response>
    [Produces(AppConfigurations.ResponseContentType)]
    [ProducesResponseType(typeof(DishModel), StatusCodes.Status200OK)]
    [HttpGet, Route("{dishId:guid}")]
    public async Task<IActionResult> FetchDish(Guid dishId)
    {
        var dishDto = await _dishService.FetchDetailsAsync(dishId);
        return Ok(_mapper.Map<DishModel>(dishDto));
    }
}