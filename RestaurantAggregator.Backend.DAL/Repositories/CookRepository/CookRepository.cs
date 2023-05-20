using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Common.CrudRepository;
using RestaurantAggregator.DAL.DbContexts;
using RestaurantAggregator.DAL.Entities.Staff;

namespace RestaurantAggregator.DAL.Repositories.CookRepository;

public class CookRepository : CrudRepository<Cook>, ICookRepository
{
    public CookRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    protected override IQueryable<Cook> PrepareToFetchDetails()
    {
        return DbSet.Include(x => x.Restaurant);
    }

    public override async Task ModifyAsync(Cook element)
    {
        var cook = FetchDetails(element.Id);

        cook.Id = element.Id;
        cook.Restaurant = element.Restaurant;
        
        await SaveChangesAsync();
    }
}