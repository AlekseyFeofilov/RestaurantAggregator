using Microsoft.AspNetCore.Authorization;
using RestaurantAggregator.Backend.DAL.Repositories.DishRepository;
using RestaurantAggregator.Backend.DAL.Repositories.ManagerRepository;
using RestaurantAggregator.Common.Extensions;

namespace RestaurantAggregator.Backend.API.AuthorizationConfigurations.AuthorizationHandlers;

public abstract class HasAccessToDishHandler<T> : AuthorizationHandler<T> where T: IAuthorizationRequirement
{
    protected readonly IManagerRepository _managerRepository;

    private readonly IDishRepository _dishRepository;

    protected readonly IHttpContextAccessor ContextAccessor;

    public HasAccessToDishHandler(IHttpContextAccessor contextAccessor, IManagerRepository managerRepository, IDishRepository dishRepository)
    {
        ContextAccessor = contextAccessor;
        _managerRepository = managerRepository;
        _dishRepository = dishRepository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, T requirement)
    {
        var manager = _managerRepository.FetchDetails(ContextAccessor.HttpContext!.User.GetNameIdentifier());
        var dish = await _dishRepository.FetchDishAsync(await GetDishId());//todo make this thing throw BadRequest but not Unauthorized
        
        if (dish.Restaurant.Id == manager.Restaurant.Id)
        {
            context.Succeed(requirement);
        }
    }

    protected abstract Task<Guid> GetDishId();
}