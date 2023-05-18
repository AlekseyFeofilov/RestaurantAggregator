using RestaurantAggregator.DAL.Entities;

namespace RestaurantAggregator.DAL.Repositories.OrderRepository;

public interface IOrderRepository
{
    Task<Order> FetchOrder(Guid orderId);
}