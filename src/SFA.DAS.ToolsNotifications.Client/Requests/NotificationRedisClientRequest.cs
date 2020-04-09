using Newtonsoft.Json;
using SFA.DAS.ToolsNotifications.Types.Entities;
using SFA.DAS.ToolsNotifications.Client.Configuration;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;
using System.Threading.Tasks;

namespace SFA.DAS.ToolsNotifications.Client.Requests
{
    public class NotificationRedisClientRequest : INotificationClientRequest
    {
        private readonly StackExchangeRedisCacheClient _cache;
        private readonly string _cacheKey;

        public NotificationRedisClientRequest(NotificationClientConfiguration configuration)
        {
            var serializer = new NewtonsoftSerializer();
            _cacheKey = configuration.RedisKey;
            _cache = new StackExchangeRedisCacheClient(serializer, configuration.RedisConnectionString);
        }

        public async Task<Notification> GetNotification()
        {
            return await _cache.GetAsync<Notification>(_cacheKey);
        }

        public async Task SetNotification(Notification notification)
        {
            await _cache.AddAsync(_cacheKey, notification);
        }
    }
}
