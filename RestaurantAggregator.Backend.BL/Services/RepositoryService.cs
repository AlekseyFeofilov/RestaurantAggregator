using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Backend.Common.Exceptions;
using RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;
using RestaurantAggregator.Backend.DAL.DbContexts;
using RestaurantAggregator.Backend.DAL.Entities;

namespace RestaurantAggregator.Backend.BL.Services;

public interface IRepositoryService
{
    Task<Dish> FetchDish(Guid id);

    Task<Order> FetchOrder(Guid id);

    Task<CartDish?> FetchCartDish(Guid dishId, Guid userId);

    Task<CartDish?> FetchCart();
}

public class RepositoryService : IRepositoryService
{
    private readonly ApplicationDbContext _context;

    public RepositoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    // public async Task<User> GetUser(ClaimsPrincipal claims)
    // {
    //     var id = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    //     var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
    //     if (user == null) throw new NotFoundException();
    //     
    //     return user;
    // }

    public async Task<Dish> FetchDish(Guid id)
    {
        var dish = await _context.Dishes.SingleOrDefaultAsync(x => x.Id == id);

        if (dish == null) throw new DishNotFoundException(id);

        return dish;
    }

    // public async Task<Order> GetOrder(Guid id)
    // {
    //     var order = await _context.Orders
    //         .Include(x => x.DishBaskets)
    //         .ThenInclude(x => x.Dish)
    //         .SingleOrDefaultAsync(x => x.Id == id);
    //
    //     if (order == null) throw new NotFoundException();
    //     return order;
    // }
    //
    // public async Task<DishBasket?> GetDishBasket(Guid dishId, Guid userId)
    // {
    //     return await _context.DishBaskets
    //         .SingleOrDefaultAsync(x =>
    //             x.User != null
    //             && x.Dish.Id == dishId
    //             && x.User.Id == userId
    //         );
    // }

    public Task<Order> FetchOrder(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<CartDish?> FetchCartDish(Guid dishId, Guid userId)
    {
        return _context.DishBaskets
            .SingleOrDefaultAsync(x =>
                x.UserId != null
                && x.Dish.Id == dishId
                && x.UserId == userId
            );
    }

    public Task<CartDish?> FetchCart()
    {
        throw new NotImplementedException();
    }
}