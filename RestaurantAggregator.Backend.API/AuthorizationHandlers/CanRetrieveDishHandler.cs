using RestaurantAggregator.Backend.API.Requirements;
using RestaurantAggregator.Backend.DAL.Repositories.DishRepository;
using RestaurantAggregator.Backend.DAL.Repositories.ManagerRepository;
using RestaurantAggregator.Common.Extensions;

namespace RestaurantAggregator.Backend.API.AuthorizationHandlers;

public class CanRetrieveDishHandler : HasAccessToDishHandler<CanRetrieveDishRequirement>
{
    public CanRetrieveDishHandler(IHttpContextAccessor contextAccessor, IManagerRepository managerRepository, IDishRepository dishRepository) : base(contextAccessor, managerRepository, dishRepository)
    {
    }

    protected override Task<Guid> GetDishId()
    {
        return Task.FromResult(Guid.Parse((string)ContextAccessor.HttpContext!.GetRouteData().Values["dishId"]!)); //crutch 
    }
}