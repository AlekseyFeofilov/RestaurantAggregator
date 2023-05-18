using RestaurantAggregator.DAL.Entities;

namespace RestaurantAggregator.DAL.Repositories.OrderRepository;

public class OrderRepository : IOrderRepository
{
    public Task<Order> FetchOrder(Guid orderId)
    {
        throw new NotImplementedException();
    }
}