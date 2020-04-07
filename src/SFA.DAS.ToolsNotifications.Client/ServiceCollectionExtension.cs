using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.ToolService.SharedNotifications.Entities;
using SFA.DAS.ToolService.SharedNotifications.Repositories;
using SFA.DAS.ToolService.SharedNotifications.Services;

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
