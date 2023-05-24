using RestaurantAggregator.Auth.Common.Exceptions.NotFoundException;
using RestaurantAggregator.Auth.DAL.DbContexts;
using RestaurantAggregator.Auth.DAL.Entities.Users;
using RestaurantAggregator.Auth.DAL.IRepositories;
using RestaurantAggregator.Common.CrudRepository;

namespace RestaurantAggregator.Auth.BL.Repositories;

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