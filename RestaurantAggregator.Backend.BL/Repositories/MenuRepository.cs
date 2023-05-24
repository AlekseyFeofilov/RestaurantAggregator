using Microsoft.EntityFrameworkCore;
using RestaurantAggregator.Backend.Common.Exceptions.NotFoundException;
using RestaurantAggregator.Backend.DAL.DbContexts;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.IRepositories;
using RestaurantAggregator.Common.CrudRepository;

namespace RestaurantAggregator.Backend.BL.Repositories;

public class MenuRepository : CrudRepository<Menu, MenuNotFoundException>, IMenuRepository
{
    public MenuRepository(ApplicationDbContext context) : base(context)
    {
    }

    protected override IQueryable<Menu> PrepareToFetchDetails()
    {
        return DbSet
            .Include(x => x.Restaurant)
            .Include(x => x.Dishes);
    }

    public override Task ModifyAsync(Menu element)
    {
        throw new NotImplementedException();
    }
}