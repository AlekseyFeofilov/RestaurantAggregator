using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using RestaurantAggregator.Notification.Extensions;
using RestaurantAggregator.Notification.Hubs;
using RestaurantAggregator.Notification.Services.MqServices;
using RestaurantAggregator.Notification.UserProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true);
    });
});

builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, IdUserIdProvider>();

builder.Services.AddMqConsumerService();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearerAuthenticationScheme();

var app = builder.Build();

Task.Run(() => { app.StartMqConsumerService(); });

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationsHub>("/notifications");

app.Run();