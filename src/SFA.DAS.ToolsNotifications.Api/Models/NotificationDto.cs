using System.Text.Json;
using System.Text.Json.Serialization;

namespace SFA.DAS.ToolsNotifications.Api.Models
{
    public struct NotificationDto : IJsonOnDeserialized
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public void OnDeserialized()
        {
           if(Title == default)
                throw new JsonException("Required property 'Title' not received in the JSON");
            else if(Description == default)
                throw new JsonException("Required property 'Description' not received in the JSON");
            else if(Enabled == default)
                throw new JsonException("Required property 'Enabled' not received in the JSON");
        }
    }
}
