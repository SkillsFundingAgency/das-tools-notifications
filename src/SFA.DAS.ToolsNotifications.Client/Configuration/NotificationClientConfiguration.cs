namespace SFA.DAS.ToolsNotifications.Client.Configuration
{
    public class NotificationClientConfiguration
    {
        public required string RedisConnectionString { get; set; }
        public required string RedisKey { get; set; }
    }
}
