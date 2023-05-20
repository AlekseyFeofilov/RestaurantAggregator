using RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;

namespace RestaurantAggregator.Auth.DAL.Repositories.UserRepository;

public interface IUserRepository
{
    public IQueryable<User> FetchAllUsers();

    public IQueryable<User> FetchAllNonStaffUsers();
    
    public User FetchUserDetails(Guid id);
}