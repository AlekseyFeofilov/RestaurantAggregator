using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;
using RestaurantAggregator.Backend.DAL.DbContexts;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Common.CrudRepository;

namespace RestaurantAggregator.Backend.DAL.Repositories.ManagerRepository;

public class ManagerRepository : CrudRepository<Manager, ManagerNotFoundException>, IManagerRepository
{
    public ManagerRepository(ApplicationDbContext context) : base(context)
    {
    }

    protected override IQueryable<Manager> PrepareToFetchDetails()
    {
        return DbSet
            .Include(x => x.Restaurant)
            .ThenInclude(x => x.Menus);
    }

    public override async Task ModifyAsync(Manager element)
    {
        var oldElement = FetchDetails(element.Id);

        oldElement.Id = element.Id;
        oldElement.Restaurant = element.Restaurant;

        await SaveChangesAsync();
    }
}