namespace SFA.DAS.ToolsNotifications.Types.Entities
{
    public class Notification
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool Enabled { get; set; }
    }
}
