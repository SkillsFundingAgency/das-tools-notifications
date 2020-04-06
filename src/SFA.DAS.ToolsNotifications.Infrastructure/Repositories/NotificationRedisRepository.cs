using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SFA.DAS.ToolsNotifications.Core.Entities;
using SFA.DAS.ToolsNotifications.Core.Repositories;
using System.Threading.Tasks;

namespace SFA.DAS.ToolsNotifications.Infrastructure.Repositories
{
    public class NotificationRedisRepository : INotificationRepository
    {
        private readonly IDistributedCache _cache;

        private readonly string _cacheKey = "das-tools-notification";

        public NotificationRedisRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<Notification> GetNotification()
        {
            var notificationJson = await _cache.GetStringAsync(_cacheKey);
            return JsonConvert.DeserializeObject<Notification>(notificationJson);
        }

        public async Task SetNotification(Notification notification)
        {
            var notificationJson = JsonConvert.SerializeObject(notification);
            await _cache.SetStringAsync(_cacheKey, notificationJson);
        }
    }
}
