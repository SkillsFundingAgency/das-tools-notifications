using System.Text.Json.Serialization;
using SFA.DAS.ToolsNotifications.Api.Models.Converters;

namespace SFA.DAS.ToolsNotifications.Api.Models
{
    [JsonConverter(typeof(NotificationDtoRequiredPropertyConverter))] 
    public struct NotificationDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }
    }
}
