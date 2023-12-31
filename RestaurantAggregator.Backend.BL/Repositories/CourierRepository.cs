using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;
using RestaurantAggregator.Backend.DAL.DbContexts;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.IRepositories;
using RestaurantAggregator.Common.CrudRepository;

namespace RestaurantAggregator.Backend.BL.Repositories;

public class CourierRepository : CrudRepository<Courier, CourierNotFoundException>, ICourierRepository
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