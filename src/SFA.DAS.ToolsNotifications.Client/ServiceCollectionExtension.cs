using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.ToolsNotifications.Client;
using SFA.DAS.ToolsNotifications.Client.Entities;
using SFA.DAS.ToolsNotifications.Client.Repositories;

namespace SFA.DAS.ToolService.SharedNotifications
{
    public static class ServiceRegistrationExtension
    {
        public static void AddSharedNotifications(this IServiceCollection services, NotificationClientConfiguration configuration)
        {
            var notificationRedisRepository = new NotificationRedisRepository(configuration);
            services.AddSingleton<INotificationClient>(new NotificationClient(notificationRedisRepository));
        }
    }
}
