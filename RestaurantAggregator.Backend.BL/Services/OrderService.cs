using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.BL.IRepositories;
using RestaurantAggregator.Common.Exceptions;
using RestaurantAggregator.Common.IServices;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.Common.Models.Enums;
using RestaurantAggregator.DAL.DbContexts;
using RestaurantAggregator.DAL.Entities;

namespace RestaurantAggregator.BL.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;
    
    private readonly IMapper _mapper;

    private readonly IOrderRepository _orderRepository;

    public OrderService(IMapper mapper, IOrderRepository orderRepository, ApplicationDbContext context)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _context = context;
    }

    public async Task<OrderDto> FetchOrder(ClaimsPrincipal claimsPrincipal, Guid orderId)
    {
        var order = await _orderRepository.FetchOrder(orderId);
        var orderDto = _mapper.Map<OrderDto>(order);
        var dishBasketsDto = await GetOrderDishBaskets(order);

        orderDto.DishBaskets = dishBasketsDto;
        return orderDto;
    }

    public async Task<IEnumerable<OrderInfoDto>> FetchAllOrders(ClaimsPrincipal claimsPrincipal, OrderOptions orderOptions)
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
        if (!cart.Any()) return;

        var firstDish = cart.First().Dish;
        var otherRestaurantDish =  cart.FirstOrDefault(cartDish => firstDish.Restaurant.Id != cartDish.Dish.Restaurant.Id);

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

    public Task RepeatOrder(ClaimsPrincipal claimsPrincipal, Guid orderId)
    {
        throw new NotImplementedException();
    }

    public Task CancelOrder(ClaimsPrincipal claimsPrincipal, Guid orderId)
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
}