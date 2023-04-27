using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestaurantAggregator.Common.Configurations;

namespace RestaurantAggregator.Common.Extensions;

public static class AddSchemes
{
    public static void AddJwtBearerAuthenticationScheme(this AuthenticationBuilder authenticationBuilder)
    {
        authenticationBuilder.AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = JwtConfigurations.Issuer,
                ValidateAudience = true,
                ValidAudience = JwtConfigurations.Audience,
                ValidateLifetime = true,
                IssuerSigningKey = JwtConfigurations.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };
        });
    }
}