using SFA.DAS.ToolsNotifications.Client.Entities;
using System.Threading.Tasks;

namespace SFA.DAS.ToolsNotifications.Client
{
    public interface INotificationClient
    {
        Task<Notification> GetNotification();
    }
}
