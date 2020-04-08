using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SFA.DAS.ToolsNotifications.Client.Configuration;
using SFA.DAS.ToolsNotifications.Client.Requests;

namespace SFA.DAS.ToolsNotifications.Client
{
    public static class ServiceRegistrationExtension
    {
        public static IServiceCollection AddNotificationClient(this IServiceCollection services, NotificationClientConfiguration configuration)
        {
            var notificationRedisRepository = new NotificationRedisClientRequest(configuration);
            services.AddSingleton<INotificationClient>(new NotificationClient(notificationRedisRepository));

            return services;
        }
    }
}
