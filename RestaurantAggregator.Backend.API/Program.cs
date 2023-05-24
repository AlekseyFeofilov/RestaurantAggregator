using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using RestaurantAggregator.Backend.API.Extensions;
using RestaurantAggregator.Backend.API.Middleware;
using RestaurantAggregator.Backend.BL.Extensions;
using RestaurantAggregator.Backend.Common.Configurations;
using RestaurantAggregator.Backend.Common.Extensions;
using RestaurantAggregator.Common.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<AppConfigurations>(builder.Configuration.GetSection("AppConfigurations"));

builder.Services.AddBL();
builder.Services.AddSwaggerService();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearerAuthenticationScheme(builder.Configuration.GetSection("JwtConfigurations").Get<JwtConfigurations>());

builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationResultTransformer>();

builder.Services.AddAutoMapper();
builder.Services.AddAuthorizationPolicies();
builder.Services.AddAuthorizationHandlers();
builder.Services.AddAuthorizationPolicyProvider();

var app = builder.Build();

app.AddBLPostBuild();
app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();