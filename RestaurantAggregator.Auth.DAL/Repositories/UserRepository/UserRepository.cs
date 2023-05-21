using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RestaurantAggregator.Auth.Common.Exceptions;
using RestaurantAggregator.Auth.Common.Models.Enums;
using RestaurantAggregator.Auth.DAL.DbContexts;
using RestaurantAggregator.Auth.DAL.Entities.IdentityEntities;
using RestaurantAggregator.Common.CrudRepository;
using RestaurantAggregator.Common.Exceptions;

namespace RestaurantAggregator.Auth.DAL.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    private readonly UserManager<User> _userManager;

    public UserRepository(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IQueryable<User> FetchAllUsers()
    {
        return _context.Users;
    }

    public IQueryable<User> FetchAllNonStaffUsers()
    {
        return _context.Users.Where(user =>
            !(_context.Managers.Any(manager => manager.Id == user.Id)
              || _context.Cooks.Any(cook => cook.Id == user.Id)
              || _context.Couriers.Any(courier => courier.Id == user.Id))
        );
    }

    public User FetchUserDetails(Guid id)
    {
        var user = FetchAllUsers().SingleOrDefault(x => x.Id == id);

        if (user == null)
        {
            throw new NotFoundException();
        }

        return user;
    }

    public async Task AddRoleAsync(Guid id, RoleType roleType)
    {
        var user = FetchUserDetails(id);
        await _userManager.AddToRoleAsync(user, roleType.ToString());
    }

    public async Task RemoveRoleAsync(Guid id, RoleType roleType)
    {
        var user = FetchUserDetails(id);
        await _userManager.RemoveFromRoleAsync(user, roleType.ToString());
    }
}