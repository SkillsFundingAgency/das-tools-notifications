using SFA.DAS.ToolsNotifications.Core.IRepositories;
using System.Threading.Tasks;
using SFA.DAS.ToolsNotifications.Client;
using SFA.DAS.ToolsNotifications.Types.Entities;

namespace SFA.DAS.ToolsNotifications.Infrastructure.Repositories
{
    public class NotificationRedisRepository : INotificationRepository
    {
        private readonly INotificationClient _cache;

        public NotificationRedisRepository(INotificationClient cache)
        {
            _cache = cache;
        }

        public async Task<Notification> GetNotification()
        {
            return await _cache.GetNotification();
        }

        public async Task SetNotification(Notification notification)
        {
            await _cache.SetNotification(notification);
        }
    }
}
