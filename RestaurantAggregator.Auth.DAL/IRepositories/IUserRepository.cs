using RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;
using RestaurantAggregator.Common.Dtos.Enums;

namespace RestaurantAggregator.Auth.DAL.IRepositories;

public interface IUserRepository
{
    public IQueryable<User> FetchAllUsers();

    public IQueryable<User> FetchAllNonStaffUsers();
    
    public User FetchUserDetails(Guid id);

    public Task AddRoleAsync(Guid id, RoleType roleType);

    public Task RemoveRoleAsync(Guid id, RoleType roleType);
}