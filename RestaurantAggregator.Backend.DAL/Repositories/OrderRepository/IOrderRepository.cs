using RestaurantAggregator.Backend.DAL.Entities;

namespace RestaurantAggregator.Backend.DAL.Repositories.OrderRepository;

public interface IOrderRepository
{
    Task<Order> FetchOrder(Guid orderId);
}