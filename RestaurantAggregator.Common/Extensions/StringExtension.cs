using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RestaurantAggregator.Common.Extensions;

public static class StringExtension
{
    public static SymmetricSecurityKey ToSymmetricSecurityKey(this string key)
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
    }
}