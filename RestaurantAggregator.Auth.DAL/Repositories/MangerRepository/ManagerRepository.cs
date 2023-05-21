using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Auth.DAL.DbContexts;
using RestaurantAggregator.Auth.DAL.Entities.Users;
using RestaurantAggregator.Common.CrudRepository;

namespace RestaurantAggregator.Auth.DAL.Repositories.MangerRepository;

public class ManagerRepository : CrudRepository<Manager>, IManagerRepository
{
    public ManagerRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task ModifyAsync(Manager element)
    {
        await SaveChangesAsync();
    }
}