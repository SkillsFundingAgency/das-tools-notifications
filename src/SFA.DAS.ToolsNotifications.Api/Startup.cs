using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SFA.DAS.ToolsNotifications.Api.Infrastructure;
using SFA.DAS.ToolsNotifications.Client;
using SFA.DAS.ToolsNotifications.Client.Configuration;
using SFA.DAS.ToolsNotifications.Core.IRepositories;
using SFA.DAS.ToolsNotifications.Core.IServices;
using SFA.DAS.ToolsNotifications.Core.Services;
using SFA.DAS.ToolsNotifications.Infrastructure.Repositories;
using System;
using System.Collections.Generic;

namespace SFA.DAS.ToolsNotifications.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            _environment = environment;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(_configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]);

            if (!ConfigurationIsLocalOrDev())
            {
                services.AddAuthentication(auth => { auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; })
                    .AddJwtBearer(auth =>
                    {
                        auth.Authority =
                            $"https://login.microsoftonline.com/{_configuration["AzureAdTenantId"]}";
                        auth.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidAudiences = new List<string>
                            {
                                _configuration["AzureADResourceId"]                            
                            }
                        };
                    });

                services.AddSingleton<IClaimsTransformation, AzureAdScopeClaimTransformation>();

                services.AddAuthorization(options =>
                {
                    options.AddPolicy("RequireNotificationRole", policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.RequireRole("Notifications");
                    });
                });
            }

            services.AddNotificationClient(_configuration.Get<NotificationClientConfiguration>());
            services.AddSingleton<INotificationRepository, NotificationRedisRepository>();
            services.AddSingleton<INotificationService, NotificationService>();

            services.AddMvc(options =>
            {
                if (!ConfigurationIsLocalOrDev())
                {
                    options.Filters.Add(new AuthorizeFilter("RequireNotificationRole"));
                }
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = _configuration["ApiName"], Version = "v1" });
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UsePathBase(_configuration["PathBase"]);
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{_configuration["PathBase"]}/swagger/v1/swagger.json", _configuration["ApiName"]);
            });

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private bool ConfigurationIsLocalOrDev()
        {
            return _configuration["EnvironmentName"].Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase) ||
                   _configuration["EnvironmentName"].Equals("DEV", StringComparison.CurrentCultureIgnoreCase) ||
                   _environment.IsDevelopment();
        }
    }
}
