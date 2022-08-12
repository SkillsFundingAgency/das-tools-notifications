using Newtonsoft.Json;

namespace SFA.DAS.ToolsNotifications.Api.Models
{
    public struct NotificationDto
    {
        [JsonProperty("title", Required = Required.Always)]
        public string Title { get; set; }

        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }

        [JsonProperty("enabled", Required = Required.Always)]
        public bool Enabled { get; set; }
    }
}
