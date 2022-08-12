using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using SFA.DAS.ToolsNotifications.Api.Infrastructure;
using SFA.DAS.ToolsNotifications.Core.IServices;
using SFA.DAS.ToolsNotifications.Client;
using SFA.DAS.ToolsNotifications.Core.Services;
using SFA.DAS.ToolsNotifications.Client.Configuration;
using Microsoft.OpenApi.Models;
using SFA.DAS.ToolsNotifications.Core.IRepositories;
using SFA.DAS.ToolsNotifications.Infrastructure.Repositories;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services.AddApplicationInsightsTelemetry();

// Configure the HTTP request pipeline.
if (!environment.IsDevelopment())
{
    builder.Services.AddAuthentication(auth => { auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; })
                    .AddJwtBearer(auth =>
                    {
                        auth.Authority =
                            $"https://login.microsoftonline.com/{configuration["AzureAdTenantId"]}";
                        auth.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidAudiences = new List<string>
                            {
                                configuration["AzureADResourceId"]
                            }
                        };
                    });

    builder.Services.AddSingleton<IClaimsTransformation, AzureAdScopeClaimTransformation>();

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("RequireNotificationRole", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireRole("Notifications");
        });
    });
}

builder.Services.AddNotificationClient(configuration.Get<NotificationClientConfiguration>());
builder.Services.AddSingleton<INotificationRepository, NotificationRedisRepository>();
builder.Services.AddSingleton<INotificationService, NotificationService>();

builder.Services.AddHealthChecks();

builder.Services.AddControllers(options =>
{
    if (!environment.IsDevelopment())
    {
        options.Filters.Add(new AuthorizeFilter("RequireNotificationRole"));
    }
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = configuration["ApiName"], Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
});

var app = builder.Build();

if (!environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
} else {
    app.UseDeveloperExceptionPage();
}

app.UsePathBase(configuration["PathBase"]);
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto
});
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"{configuration["PathBase"]}/swagger/v1/swagger.json", configuration["ApiName"]);
});
app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();
app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
