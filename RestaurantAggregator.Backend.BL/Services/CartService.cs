using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Common.Exceptions;
using RestaurantAggregator.Common.IServices;
using RestaurantAggregator.Common.Models.Dto;
using RestaurantAggregator.DAL.DbContexts;
using RestaurantAggregator.DAL.Entities;

namespace RestaurantAggregator.BL.Services;

public class CartService : ICartService
{
    private readonly ApplicationDbContext _context;
    private readonly IRepositoryService _repositoryService;
    private readonly IMapper _mapper;

    public CartService(ApplicationDbContext context, IRepositoryService repositoryService, IMapper mapper)
    {
        _context = context;
        _repositoryService = repositoryService;
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
        var dishBasket = await _repositoryService.FetchCartDish(dishId, userId);
        var dish = await _repositoryService.FetchDish(dishId);
        await AddDishBasket(dishBasket, dish, userId);
    }

    public async Task RemoveDish(ClaimsPrincipal claimsPrincipal, Guid dishId, bool increase = false)
    {
        var userId = Guid.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var cartDish = await _repositoryService.FetchCartDish(dishId, userId);

        if (cartDish == null)
        {
            throw new DishNotFoundException();
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