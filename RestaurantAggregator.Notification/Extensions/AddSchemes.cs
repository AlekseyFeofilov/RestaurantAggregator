using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace RestaurantAggregator.Notification.Extensions;

public static class AddSchemes
{
    public static void AddJwtBearerAuthenticationScheme(this AuthenticationBuilder authenticationBuilder)
    {
        authenticationBuilder.AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
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
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) &&
                        (path.StartsWithSegments("/notifications")))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });
    }
    
    private static class JwtConfigurations
    {
        public const string Issuer = "Issuer"; 
        public const string Audience = "Audience";
        private const string Key = "SuperPuperSecretKeyIsSoLongBeacuseSymmetricSignatureProviderThrowAnErrorWhenItsTooShort";  
        public const int Lifetime = 1440;
    
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}