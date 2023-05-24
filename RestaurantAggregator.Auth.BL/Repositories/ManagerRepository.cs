using RestaurantAggregator.Auth.Common.Exceptions.NotFoundException;
using RestaurantAggregator.Auth.DAL.DbContexts;
using RestaurantAggregator.Auth.DAL.Entities.Users;
using RestaurantAggregator.Auth.DAL.IRepositories;
using RestaurantAggregator.Common.CrudRepository;

namespace RestaurantAggregator.Auth.BL.Repositories;

public class ManagerRepository : CrudRepository<Manager, ManagerNotFoundException>, IManagerRepository
{
    public ManagerRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task ModifyAsync(Manager element)
    {
        await SaveChangesAsync();
    }
}