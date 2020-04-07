using SFA.DAS.ToolService.SharedNotifications.Entities;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;
using System.Threading.Tasks;

namespace SFA.DAS.ToolService.SharedNotifications.Repositories
{
    public class NotificationRedisRepository : INotificationRepository
    {
        private readonly StackExchangeRedisCacheClient _cache;
        private readonly string _cacheKey;

        public NotificationRedisRepository(NotificationClientConfiguration configuration)
        {
            var serializer = new NewtonsoftSerializer();
            _cacheKey = configuration.RedisKey;
            _cache = new StackExchangeRedisCacheClient(serializer, configuration.RedisConnectionString);
        }

        public async Task<Notification> GetNotification()
        {
            return await _cache.GetAsync<Notification>(_cacheKey);
        }
    }
}
