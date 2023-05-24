using Microsoft.AspNetCore.Authorization;
using RestaurantAggregator.Backend.API.AuthorizationConfigurations.AuthorizationFailureReasons;
using RestaurantAggregator.Backend.DAL.Entities;
using RestaurantAggregator.Backend.DAL.IRepositories;
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
        var dishId = await GetDishId();
        Dish dish;
        
        try
        {
            dish = await _dishRepository.FetchDishAsync(dishId);
        }
        catch (Exception e)
        {
            context.Fail(new NotFoundReason(this, $"Order with id {dishId} not found"));
            throw;
        }

        if (dish.Restaurant.Id == manager.Restaurant.Id)
        {
            context.Succeed(requirement);
        }
    }

    protected abstract Task<Guid> GetDishId();
}