using RestaurantAggregator.Auth.Common.Exceptions.NotFoundExceptions;
using RestaurantAggregator.Auth.DAL.DbContexts;
using RestaurantAggregator.Auth.DAL.Entities.Users;
using RestaurantAggregator.Auth.DAL.IRepositories;
using RestaurantAggregator.Common.CrudRepository;

namespace RestaurantAggregator.Auth.BL.Repositories;

public class CookRepository : CrudRepository<Cook, CourierNotFoundException>, ICookRepository
{
    public CookRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override Task ModifyAsync(Cook element)
    {
        throw new NotImplementedException();
    }
}