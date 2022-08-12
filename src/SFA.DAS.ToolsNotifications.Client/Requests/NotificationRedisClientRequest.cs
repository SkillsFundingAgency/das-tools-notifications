using Newtonsoft.Json;
using SFA.DAS.ToolsNotifications.Types.Entities;
using SFA.DAS.ToolsNotifications.Client.Configuration;
using StackExchange.Redis;

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
            return JsonConvert.DeserializeObject<Notification>(notificationJson);
        }

        public async Task SetNotification(Notification notification)
        {
            var notificationJson = JsonConvert.SerializeObject(notification);
            await _redis.GetDatabase().StringSetAsync(_cacheKey, notificationJson);
        }
    }
}
