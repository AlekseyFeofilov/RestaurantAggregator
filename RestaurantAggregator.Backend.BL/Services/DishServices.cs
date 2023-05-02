using System.Security.Claims;
using AutoMapper;
using RestaurantAggregator.Common.Configurations;
using RestaurantAggregator.Common.Exceptions;
using RestaurantAggregator.Common.Extensions;
using RestaurantAggregator.Common.IServices;
using RestaurantAggregator.Common.Models;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.Common.Models.Dto.Dish;
using RestaurantAggregator.DAL.Entities;
using RestaurantAggregator.DAL.Models;
using RestaurantAggregator.DAL.Repositories.DishRepository;
using RestaurantAggregator.DAL.Repositories.RestaurantRepository;
using RestaurantAggregator.DAL.Repositories.ReviewRepository;

namespace RestaurantAggregator.BL.Services;

public class DishServices : IDishService
{
    private readonly IMapper _mapper;

    private const int PageSize = AppConfigurations.PageSize;

    private readonly IDishRepository _dishRepository;

    private readonly IReviewRepository _reviewRepository;

    private readonly IRestaurantRepository _restaurantRepository;

    public DishServices(IMapper mapper, IDishRepository dishRepository, IReviewRepository reviewRepository, IRestaurantRepository restaurantRepository)
    {
        _mapper = mapper;
        _dishRepository = dishRepository;
        _reviewRepository = reviewRepository;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<PagedEnumerable<DishDto>> FetchAllDishesAsync(DishOptions dishOptions)
    {
        var fetchDishOptions = _mapper.Map<FetchDishOptions>(dishOptions);

        var dishes = await _dishRepository.FetchAllDishesAsync(fetchDishOptions);
        var dishDtos = dishes.Select(x => _mapper.Map<DishDto>(x));

        return dishDtos.GetPagedEnumerable(PageSize, dishOptions.Page);
    }

    public async Task<DishDto> FetchDishAsync(Guid dishId)
    {
        await _reviewRepository.FetchReviews(dishId);
        var dish = await _dishRepository.FetchDishAsync(dishId);

        return _mapper.Map<DishDto>(dish);
    }

    public async Task CreateDishAsync(DishCreateDto dishCreateDto)
    {
        var restaurant = await _restaurantRepository.FetchRestaurant(dishCreateDto.RestaurantId);
        var dish = _mapper.Map<Dish>(dishCreateDto);
        dish.Restaurant = restaurant;

        await _dishRepository.CreateDishAsync(dish);
    }

    public async Task ModifyDishAsync(DishModifyDto dishModifyDto)
    {
        var restaurant = await _restaurantRepository.FetchRestaurant(dishModifyDto.RestaurantId);
        var dish = _mapper.Map<Dish>(dishModifyDto);
        dish.Restaurant = restaurant;
        
        await _dishRepository.ModifyDishAsync(dish);
    }

    public Task DeleteDishAsync(Guid dishId)
    {
        return _dishRepository.DeleteDishAsync(dishId);
    }
}