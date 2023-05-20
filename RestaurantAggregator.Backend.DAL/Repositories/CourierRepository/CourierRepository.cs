using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Common.CrudRepository;
using RestaurantAggregator.DAL.DbContexts;
using RestaurantAggregator.DAL.Entities.Staff;

namespace RestaurantAggregator.DAL.Repositories.CourierRepository;

public class CourierRepository : CrudRepository<Courier>, ICourierRepository
{
    public CourierRepository(ApplicationDbContext context) : base(context)
    {
    }

    protected override IQueryable<Courier> PrepareToFetchDetails()
    {
        return DbSet.Include(x => x.Restaurant);
    }

    public override async Task ModifyAsync(Courier element)
    {
        var courier = FetchDetails(element.Id);

        courier.Id = element.Id;
        courier.Restaurant = element.Restaurant;
        
        await SaveChangesAsync();
    }
}