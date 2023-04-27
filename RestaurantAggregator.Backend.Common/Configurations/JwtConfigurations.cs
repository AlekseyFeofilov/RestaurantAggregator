using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RestaurantAggregator.Common.Configurations;

public static class JwtConfigurations
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
