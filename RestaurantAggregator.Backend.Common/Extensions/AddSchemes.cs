using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Common.Configurations;
using RestaurantAggregator.Common.Extensions;

namespace RestaurantAggregator.Backend.Common.Extensions;

public static class AddSchemes
{
    public static void AddJwtBearerAuthenticationScheme(this AuthenticationBuilder authenticationBuilder, JwtConfigurations jwtConfigurations)
    {
        authenticationBuilder.AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtConfigurations.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtConfigurations.Audience,
                ValidateLifetime = true,
                IssuerSigningKey = jwtConfigurations.Key.ToSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };
        });
    }
}