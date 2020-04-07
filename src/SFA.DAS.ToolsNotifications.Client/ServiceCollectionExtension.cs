using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SFA.DAS.ToolsNotifications.Client.Entities;
using SFA.DAS.ToolsNotifications.Client.Repositories;

namespace SFA.DAS.ToolsNotifications.Client
{
    public static class ServiceRegistrationExtension
    {
        public static IServiceCollection AddNotificationClient(this IServiceCollection services, IOptions<NotificationClientConfiguration> configuration)
        {
            var notificationRedisRepository = new NotificationRedisRepository(configuration.Value);
            services.AddSingleton<INotificationClient>(new NotificationClient(notificationRedisRepository));

            return services;
        }
    }
}
