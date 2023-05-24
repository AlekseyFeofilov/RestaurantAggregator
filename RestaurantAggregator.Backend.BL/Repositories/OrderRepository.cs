using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;
using RestaurantAggregator.Backend.DAL.DbContexts;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.IRepositories;

namespace RestaurantAggregator.Backend.BL.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order> FetchOrderAsync(Guid orderId)
    {
        var order = await _context.Orders
            .Include(x => x.Restaurant)
            .Include(x => x.Cook).ThenInclude(x => x.Restaurant)
            .Include(x => x.Courier).ThenInclude(x => x.Restaurant)
            .SingleOrDefaultAsync(x => x.Id == orderId);
        
        if (order == null)
        {
            throw new OrderNotFoundException(orderId);
        }

        return order;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}