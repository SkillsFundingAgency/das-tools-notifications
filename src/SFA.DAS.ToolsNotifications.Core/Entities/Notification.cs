using Newtonsoft.Json;

namespace SFA.DAS.ToolsNotifications.Core.Entities
{
    public class Notification
    {
        [JsonProperty("title", Required = Required.Always)]
        public string Title { get; set; }

        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }

        [JsonProperty("enabled", Required = Required.Always)]
        public bool Enabled { get; set; }
    }
}
