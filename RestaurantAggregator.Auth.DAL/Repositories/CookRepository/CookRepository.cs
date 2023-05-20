using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Auth.DAL.DbContexts;
using RestaurantAggregator.Auth.DAL.Entities.Users;
using RestaurantAggregator.Common.CrudRepository;

namespace RestaurantAggregator.Auth.DAL.Repositories.CookRepository;

public class CookRepository : CrudRepository<Cook>, ICookRepository
{
    public CookRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override Task ModifyAsync(Cook element)
    {
        throw new NotImplementedException();
    }
}