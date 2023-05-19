using RestaurantAggregator.Common.CrudRepository;
using RestaurantAggregator.Common.Models;
using RestaurantAggregator.DAL.DbContexts;
using RestaurantAggregator.DAL.Entities.Staff;

namespace RestaurantAggregator.DAL.Repositories.ManagerRepository;

public class ManagerRepository : CrudRepository<Manager>, IManagerRepository
{
    public ManagerRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task ModifyAsync(Manager element)
    {
        var oldElement = FetchDetails(element.Id);

        oldElement.Id = element.Id;
        oldElement.Restaurant = element.Restaurant;
        
        await SaveChangesAsync();
    }
}