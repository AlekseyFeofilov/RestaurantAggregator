using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;
using RestaurantAggregator.Backend.DAL.DbContexts;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Common.CrudRepository;

namespace RestaurantAggregator.Backend.DAL.Repositories.CookRepository;

public class CookRepository : CrudRepository<Cook, CookNotfoundException>, ICookRepository
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