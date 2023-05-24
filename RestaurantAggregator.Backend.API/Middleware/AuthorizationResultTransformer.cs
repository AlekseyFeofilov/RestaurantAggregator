using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace RestaurantAggregator.Backend.API.Middleware;

public class AuthorizationResultTransformer : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationResultTransformer _defaultTransformer = new();

    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Forbidden)
        {
            // if (authorizeResult.AuthorizationFailure?.FailureReasons.Any(x => x is AuthorizationFailureReason) ?? false)
            // {
            //     context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            //     return;
            // }
        }

        await _defaultTransformer.HandleAsync(next, context, policy, authorizeResult);
    }
}