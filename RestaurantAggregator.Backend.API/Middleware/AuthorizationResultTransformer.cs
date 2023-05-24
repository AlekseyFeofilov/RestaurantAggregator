using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using RestaurantAggregator.Backend.API.AuthorizationConfigurations.AuthorizationFailureReasons;

namespace RestaurantAggregator.Backend.API.Middleware;

public class AuthorizationResultTransformer : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _defaultTransformer = new();

    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Forbidden)
        {
            if (authorizeResult.AuthorizationFailure?.FailureReasons.Any(x => x is NotFoundReason) ?? false)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }
        }

        await _defaultTransformer.HandleAsync(next, context, policy, authorizeResult);
    }
}