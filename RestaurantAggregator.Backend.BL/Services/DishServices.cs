using AutoMapper;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Dto;
using RestaurantAggregator.Backend.Common.Dto.Dish;
using RestaurantAggregator.Backend.Common.IServices;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.Models;
using RestaurantAggregator.Backend.DAL.Repositories.DishRepository;
using RestaurantAggregator.Backend.DAL.Repositories.RestaurantRepository;
using RestaurantAggregator.Backend.DAL.Repositories.ReviewRepository;
using RestaurantAggregator.Common.Models;

namespace RestaurantAggregator.Backend.BL.Services;

public class DishServices : IDishService
{
    private readonly IMapper _mapper;

    private const int PageSize = AppConfigurations.PageSize;

    private readonly IDishRepository _dishRepository;

    private readonly IReviewRepository _reviewRepository;

    private readonly IRestaurantRepository _restaurantRepository;

    public DishServices(IMapper mapper, IDishRepository dishRepository, IReviewRepository reviewRepository,
        IRestaurantRepository restaurantRepository)
    {
        _mapper = mapper;
        _dishRepository = dishRepository;
        _reviewRepository = reviewRepository;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<PagedEnumerable<DishDto>> FetchAllDishesAsync(DishOptions dishOptions)
    {
        var fetchDishOptions = _mapper.Map<FetchDishOptions>(dishOptions);

        var pagedDishes = await _dishRepository.FetchAllDishesAsync(fetchDishOptions);
        var pagedDishDtos = new PagedEnumerable<DishDto>(
            pagedDishes.Items.Select(x => _mapper.Map<DishDto>(x)),
            pagedDishes.Pagination
        );

        return pagedDishDtos;
    }

    public async Task<DishDto> FetchDishAsync(Guid dishId)
    {
        await _reviewRepository.FetchReviews(dishId);
        var dish = await _dishRepository.FetchDishAsync(dishId);

        return _mapper.Map<DishDto>(dish);
    }

    public async Task CreateDishAsync(DishCreateDto dishCreateDto)
    {
        var restaurant = await _restaurantRepository.FetchRestaurantAsync(dishCreateDto.RestaurantId);
        var dish = _mapper.Map<Dish>(dishCreateDto);
        dish.Restaurant = restaurant;

        await _dishRepository.CreateDishAsync(dish);
    }

    public async Task ModifyDishAsync(DishModifyDto dishModifyDto)
    {
        var restaurant = await _restaurantRepository.FetchRestaurantAsync(dishModifyDto.RestaurantId);
        var dish = _mapper.Map<Dish>(dishModifyDto);
        dish.Restaurant = restaurant;

        await _dishRepository.ModifyDishAsync(dish);
    }

    public Task DeleteDishAsync(Guid dishId)
    {
        return _dishRepository.DeleteDishAsync(dishId);
    }
}