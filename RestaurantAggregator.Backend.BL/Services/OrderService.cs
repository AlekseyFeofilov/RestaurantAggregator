using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Backend.Common.Dto;
using RestaurantAggregator.Backend.Common.Exceptions;
using RestaurantAggregator.Backend.Common.Exceptions.BadRequestExceptions;
using RestaurantAggregator.Backend.Common.IServices;
using RestaurantAggregator.Backend.DAL.DbContexts;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.Repositories.ReviewRepository;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Backend.BL.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    private readonly IMapper _mapper;

    private readonly IReviewRepository _reviewRepository;

    private readonly IUserService _userService;

    public OrderService(IMapper mapper, ApplicationDbContext context, IReviewRepository reviewRepository,
        IUserService userService)
    {
        _mapper = mapper;
        _context = context;
        _reviewRepository = reviewRepository;
        _userService = userService;
    }

    public async Task<OrderDto> FetchOrder(Guid orderId) //todo заказ возвращает id DishInCart, а не id Dish 
    {
        var order = await _context.Orders
            .Include(x => x.DishBaskets)
            .ThenInclude(x => x.Dish)
            .SingleOrDefaultAsync(x => x.Id == orderId);

        var orderDto = _mapper.Map<OrderDto>(order);
        return orderDto;
    }

    public async Task<IEnumerable<OrderInfoDto>> FetchAllOrders(ClaimsPrincipal claimsPrincipal,
        OrderOptions orderOptions)
    {
        var userId = Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        return await _context.Orders
            .Where(x => x.UserId == userId)
            .Select(x => new OrderInfoDto(x.Id, x.DeliveryTime, x.OrderTime, x.Status, x.Price))
            .ToListAsync();
    }

    public async Task CreateOrder(ClaimsPrincipal claimsPrincipal, OrderCreateDto orderCreateDto)
    {
        var userId = Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var cart = await GetOrderDishBaskets(userId);

        if (cart.Any(x => !x.Dish.Active || x.Dish.Deleted)) throw new DishInCartNotAvailableException(); //todo make message: which one
        if (!cart.Any()) throw new CartIsEmptyException();

        var firstDish = cart.First().Dish;
        var otherRestaurantDish =
            cart.FirstOrDefault(cartDish => firstDish.Restaurant.Id != cartDish.Dish.Restaurant.Id);

        if (otherRestaurantDish != null)
        {
            throw new DishFromDifferentRestaurantsException();
        }

        await _context.Orders.AddAsync(CreateOrder(orderCreateDto, cart, userId));
        EmptyUserCart(userId);

        await _context.SaveChangesAsync();
    }

    public void ConfirmOrderDelivery(Order order)
    {
        order.Status = OrderStatus.Delivered;
        _context.SaveChangesAsync();
    }

    private async Task<ICollection<DishInCartDto>> GetOrderDishBaskets(Order order)
    {
        return await _context.DishBaskets
            .Where(x => order.DishBaskets.Contains(x))
            .Select(x => _mapper.Map<DishInCartDto>(x))
            .ToListAsync();
    }

    private async Task<ICollection<CartDish>> GetOrderDishBaskets(Guid userId)
    {
        return await _context.DishBaskets
            .Where(x => x.UserId == userId)
            .Include(x => x.Dish)
            .ThenInclude(x => x.Restaurant)
            .ToListAsync();
    }

    private Order CreateOrder(OrderCreateDto orderCreateDto, ICollection<CartDish> cart, Guid userId)
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            DeliveryTime = orderCreateDto.DeliveryTime,
            OrderTime = DateTime.Now,
            Status = OrderStatus.Created,
            Price = cart.Sum(x => x.Amount * x.Dish.Price),
            DishBaskets = cart,
            Address = orderCreateDto.Address,
            UserId = userId
        };
    }

    private static void EmptyUserCart(Guid userId)
    {
        // foreach (var dishBasket in user.Cart)
        // {
        //     dishBasket.User = null;
        // }
    }

    public Task RepeatOrder(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task CancelOrder(Guid orderId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<OrderDto>> FetchCurrentOrder(ClaimsPrincipal claimsPrincipal)
    {
        var userId = Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        return await _context.Orders
            .Where(x => x.UserId == userId && x.Status != OrderStatus.Canceled && x.Status != OrderStatus.Delivered)
            .Include(x => x.DishBaskets)
            .Select(x => _mapper.Map<OrderDto>(x))
            .ToListAsync();
    }

    public Task<bool> CheckReviewAccess(ClaimsPrincipal claimsPrincipal, Guid dishId)
    {
        var userId = _userService.GetUserId(claimsPrincipal);
        return _reviewRepository.IsAnyOrderWithDish(userId, dishId);
    }

    public async Task SetReview(ClaimsPrincipal claimsPrincipal, Guid dishId, int rating)
    {
        var userId = _userService.GetUserId(claimsPrincipal);

        var dish = await _context.Dishes.SingleOrDefaultAsync(x => x.Id == dishId);
        if (dish == null || !dish.Active || dish.Deleted) throw new DishNotFoundException();

        var review = await FetchReview(dishId, userId);
        await SetReview(review, dish, userId, rating);
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