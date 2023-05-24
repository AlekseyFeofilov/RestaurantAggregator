using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Backend.Common.Dtos.Cart;
using RestaurantAggregator.Backend.Common.Exceptions;
using RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;
using RestaurantAggregator.Backend.Common.IServices;
using RestaurantAggregator.Backend.DAL.DbContexts;
using RestaurantAggregator.Backend.DAL.Entities;

namespace RestaurantAggregator.Backend.BL.Services;

public class CartService : ICartService
{
    private readonly ApplicationDbContext _context;

    private readonly IMapper _mapper;

    public CartService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DishInCartDto>> FetchCart(ClaimsPrincipal claimsPrincipal)
    {
        var userId = Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var dishBaskets = await _context.DishBaskets
            .Where(x => x.UserId == userId)
            .Include(x => x.Dish)
            .ThenInclude(x => x.Reviews)
            .ToListAsync();

        return dishBaskets.Select(x => _mapper.Map<DishInCartDto>(x));
    }

    public async Task AddDish(ClaimsPrincipal claimsPrincipal, Guid dishId)
    {
        var userId = Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var dishBasket = await _context.DishBaskets
            .SingleOrDefaultAsync(x =>
                x.UserId != null
                && x.Dish.Id == dishId
                && x.UserId == userId
            );
        
        var dish = await _context.Dishes.SingleOrDefaultAsync(x => x.Id == dishId);

        if (dish == null) throw new DishNotFoundException(dishId);
        
        await AddDishBasket(dishBasket, dish, userId);
    }

    public async Task RemoveDish(ClaimsPrincipal claimsPrincipal, Guid dishId, bool increase = false)
    {
        var userId = Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var cartDish = await _context.DishBaskets
            .SingleOrDefaultAsync(x =>
                x.UserId != null
                && x.Dish.Id == dishId
                && x.UserId == userId
            );

        if (cartDish == null)
        {
            throw new DishNotFoundException(dishId);
        }
        
        if (!increase || cartDish.Amount == 1)
        {
            _context.DishBaskets.Remove(cartDish);
        }
        else
        {
            cartDish.Amount--;
        }

        await _context.SaveChangesAsync();
    }

    private Task AddDishBasket(CartDish? dishBasket, Dish dish, Guid userId)
    {
        if (dishBasket != null)
        {
            dishBasket.Amount++;
        }
        else
        {
            if (!dish.Active || dish.Deleted) throw new DishNotFoundException(dish.Id);
            
            _context.DishBaskets.Add(new CartDish
            {
                Amount = 1,
                Dish = dish,
                Id = new Guid(),
                UserId = userId
            });
        }

        return _context.SaveChangesAsync();
    }
}