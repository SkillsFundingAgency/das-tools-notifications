using SFA.DAS.ToolsNotifications.Types.Entities;
using System.Threading.Tasks;

namespace SFA.DAS.ToolsNotifications.Client
{
    public interface INotificationClient
    {
        Task<Notification> GetNotification();

        Task SetNotification(Notification notification);
    }
}
