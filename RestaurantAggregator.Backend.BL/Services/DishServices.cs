using System.Security.Claims;
using AutoMapper;
using RestaurantAggregator.Common.Configurations;
using RestaurantAggregator.Common.Exceptions;
using RestaurantAggregator.Common.IServices;
using RestaurantAggregator.Common.Models;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.DAL.Entities;
using RestaurantAggregator.DAL.Models;
using RestaurantAggregator.DAL.Repositories.DishRepository;
using RestaurantAggregator.DAL.ReviewRepository;

namespace RestaurantAggregator.BL.Services;

public class DishServices : IDishService
{
    private readonly IMapper _mapper;

    private const int PageSize = AppConfigurations.PageSize;

    private readonly IRepositoryService _repositoryService;

    private readonly IDishRepository _dishRepository;

    private readonly IReviewRepository _reviewRepository;

    private readonly IUserService _userService;

    public DishServices(IMapper mapper, IRepositoryService repositoryService, IUserService userService,
        IDishRepository dishRepository, IReviewRepository reviewRepository)
    {
        _mapper = mapper;
        _repositoryService = repositoryService;
        _userService = userService;
        _dishRepository = dishRepository;
        _reviewRepository = reviewRepository;
    }

    public async Task<DishPagedListDto> FetchAllDishes(DishOptions dishOptions)
    {
        var fetchDishOptions = _mapper.Map<FetchDishOptions>(dishOptions);
        fetchDishOptions.Skip = (dishOptions.Page - 1) * PageSize;
        fetchDishOptions.Take = PageSize;
        
        var dishes = await _dishRepository.FetchAllDishes(fetchDishOptions);
        
        return GetDishPagedList(dishes.Count, dishes.Select(x => _mapper.Map<DishDto>(x)), dishOptions.Page);
    }

    public async Task<DishDto> FetchDish(Guid dishId)
    {
        await _reviewRepository
            .FetchReviews(dishId); //todo проверить, работает ли это. Возможно, нужно сделать "var _ ="
        var dish = await _repositoryService.FetchDish(dishId);

        return _mapper.Map<DishDto>(dish);
    }

    public bool CheckReviewAccess(ClaimsPrincipal claimsPrincipal, Guid dishId)
    {
        var userId = _userService.GetUserId(claimsPrincipal);
        return _reviewRepository.IsAnyOrderWithDish(userId, dishId);
    }

    public async Task SetReview(ClaimsPrincipal claimsPrincipal, Guid dishId, int rating)
    {
        var userId = _userService.GetUserId(claimsPrincipal);
        var dish = await _repositoryService.FetchDish(dishId);
        var review = await FetchReview(dishId, userId); 
        await SetReview(review, dish, userId, rating);
    }

    private DishPagedListDto GetDishPagedList(int dishTotalCount, IEnumerable<DishDto> dishPage, int page)
    {
        var pageCount = (int)Math.Ceiling(dishTotalCount * 1.0 / PageSize);
        var dishCount = page < pageCount ? PageSize : Math.Max(0, dishTotalCount - PageSize * (page - 1));
        if (dishCount == 0) throw new DishNotFoundException(); //crutch: нужна свой тип ошибки

        return new DishPagedListDto(
            dishPage,
            new PageInfoModel(
                dishCount,
                pageCount,
                page
            )
        );
    }

    private async Task<Review?> FetchReview(Guid dishId, Guid userId)
    {
        return await _reviewRepository.FetchReview(dishId, userId);
    }

    private async Task SetReview(Review? review, Dish dish, Guid userId, int rating)
    {
        if (review != null)
        {
            await _reviewRepository.UpdateReview(review, rating);
        }
        else
        {
            await _reviewRepository.CreateReview(CreateReview(dish, userId, rating));
        }
    }

    private Review CreateReview(Dish dish, Guid userId, int rating)
    {
        return new Review
        {
            Id = Guid.NewGuid(),
            Dish = dish,
            UserId = userId,
            Rating = rating
        };
    }
}