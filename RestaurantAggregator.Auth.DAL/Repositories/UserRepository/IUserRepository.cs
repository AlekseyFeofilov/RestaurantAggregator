using System.Security.Claims;
using RestaurantAggregator.Auth.Common.Models.Enums;
using RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;

namespace RestaurantAggregator.Auth.DAL.Repositories.UserRepository;

public interface IUserRepository
{
    public IQueryable<User> FetchAllUsers();

    public IQueryable<User> FetchAllNonStaffUsers();
    
    public User FetchUserDetails(Guid id);

    public Task AddRoleAsync(Guid id, RoleType roleType);

    public Task RemoveRoleAsync(Guid id, RoleType roleType);
}