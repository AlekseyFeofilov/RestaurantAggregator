using RestaurantAggregator.Backend.DAL.Entities;

namespace RestaurantAggregator.Backend.DAL.Repositories.OrderRepository;

public class OrderRepository : IOrderRepository
{
    public Task<Order> FetchOrder(Guid orderId)
    {
        throw new NotImplementedException();
    }
}