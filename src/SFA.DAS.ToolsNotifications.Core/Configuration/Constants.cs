namespace SFA.DAS.ToolsNotifications.Core.Configuration
{
    public static class Constants
    {
        public const string ApiName = "Tool Service Notification API";
        public const string PathBase = "/api/notifications";
        public const string RedisKey = "das-tools-notification";
        public const string AuthorizationPolicyName = "RequireNotificationRole";
        public const string AuthorizationRequiredRoleName = "Notifications";
        public const string ScopeClaimType = "http://schemas.microsoft.com/identity/claims/scope";
        public const string ObjectIdClaimType = "http://schemas.microsoft.com/identity/claims/objectidentifier";
    }
}
