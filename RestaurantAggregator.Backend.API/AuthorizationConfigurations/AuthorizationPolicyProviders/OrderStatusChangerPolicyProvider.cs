using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestaurantAggregator.Backend.API.AuthorizationConfigurations.Requirements;
using RestaurantAggregator.Common.Models.Enums;

namespace RestaurantAggregator.Backend.API.AuthorizationConfigurations.AuthorizationPolicyProviders;

public class OrderStatusChangerPolicyProvider : DefaultAuthorizationPolicyProvider
{
    private readonly AuthorizationOptions _options;


    public OrderStatusChangerPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        _options = options.Value;
    }
    
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);
        if (policy is not null) return policy;
        
        var orderStatus = JsonConvert.DeserializeObject<OrderStatus>(policyName);
        policy = new AuthorizationPolicyBuilder()
            .AddRequirements(new CanSetOrderStatusRequirement(orderStatus)).Build();
            
        _options.AddPolicy(policyName, policy);
        return policy;
    }

}