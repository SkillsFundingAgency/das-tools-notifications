using SFA.DAS.ToolsNotifications.Types.Entities;
using SFA.DAS.ToolsNotifications.Client.Configuration;
using StackExchange.Redis;
using System.Text.Json;

namespace SFA.DAS.ToolsNotifications.Client.Requests
{
    public class NotificationRedisClientRequest : INotificationClientRequest
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly string _cacheKey;

        public NotificationRedisClientRequest(NotificationClientConfiguration configuration)
        {
            _cacheKey = configuration.RedisKey;
            _redis = ConnectionMultiplexer.Connect(configuration.RedisConnectionString);
        }

        public async Task<Notification> GetNotification()
        {
            var notificationJson = await _redis.GetDatabase().StringGetAsync(_cacheKey);
            return JsonSerializer.Deserialize<Notification>(notificationJson);
        }

        public async Task SetNotification(Notification notification)
        {
            var notificationJson = JsonSerializer.Serialize(notification);
            await _redis.GetDatabase().StringSetAsync(_cacheKey, notificationJson);
        }
    }
}
