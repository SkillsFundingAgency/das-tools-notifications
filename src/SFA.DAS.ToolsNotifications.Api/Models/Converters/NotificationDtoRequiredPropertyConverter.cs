using System.Text.Json;
using System.Text.Json.Serialization;

namespace SFA.DAS.ToolsNotifications.Api.Models.Converters
{
    public class NotificationDtoRequiredPropertyConverter : JsonConverter<NotificationDto>
    {
        public override NotificationDto Read(
            ref Utf8JsonReader reader,
            Type type,
            JsonSerializerOptions options)
        {
            // Don't pass in options when recursively calling Deserialize.
            var notificationDto = JsonSerializer.Deserialize<NotificationDto>(ref reader)!;

            // Check for required fields set by values in JSON
            if(notificationDto!.Title == default)
                throw new JsonException("Required property 'Title' not received in the JSON");
            else if(notificationDto!.Description == default)
                throw new JsonException("Required property 'Description' not received in the JSON");
            else if(notificationDto!.Enabled == default)
                throw new JsonException("Required property 'Enabled' not received in the JSON");
            else    
                return notificationDto;
        }

        public override void Write(
            Utf8JsonWriter writer,
            NotificationDto notificationDto, JsonSerializerOptions options)
        {
            // Don't pass in options when recursively calling Serialize.
            JsonSerializer.Serialize(writer, notificationDto);
        }
    }
}
