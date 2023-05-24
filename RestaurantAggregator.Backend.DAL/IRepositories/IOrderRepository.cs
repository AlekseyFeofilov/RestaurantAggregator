using RestaurantAggregator.Backend.DAL.Entities;

namespace RestaurantAggregator.Backend.DAL.IRepositories;

public interface IOrderRepository
{
    Task<Order> FetchOrderAsync(Guid orderId);

    Task SaveChangesAsync();
}