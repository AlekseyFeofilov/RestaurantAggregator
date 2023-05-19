using RestaurantAggregator.Auth.DAL.DbContexts;
using RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;
using RestaurantAggregator.Common.CrudRepository;
using RestaurantAggregator.Common.Exceptions;

namespace RestaurantAggregator.Auth.DAL.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<User> FetchAllElements()
    {
        return _context.Users;
    }

    public User FetchDetails(Guid id)
    {
        var user = FetchAllElements().SingleOrDefault(x => x.Id == id);

        if (user == null)
        {
            throw new NotFoundException();
        }

        return user;
    }
}