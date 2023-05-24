using RestaurantAggregator.Auth.Common.Exceptions.NotFoundException;
using RestaurantAggregator.Auth.DAL.DbContexts;
using RestaurantAggregator.Auth.DAL.Entities.Users;
using RestaurantAggregator.Common.CrudRepository;

namespace RestaurantAggregator.Auth.DAL.Repositories.CourierRepository;

public class CourierRepository : CrudRepository<Courier, CourierNotFoundException>, ICourierRepository
{
    public CourierRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override Task ModifyAsync(Courier element)
    {
        throw new NotImplementedException();
    }
}