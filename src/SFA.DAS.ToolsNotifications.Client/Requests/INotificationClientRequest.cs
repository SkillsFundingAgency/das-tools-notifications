using SFA.DAS.ToolsNotifications.Types.Entities;
using System.Threading.Tasks;

namespace SFA.DAS.ToolsNotifications.Client.Requests
{
    public interface INotificationClientRequest
    {
        Task<Notification> GetNotification();

        Task SetNotification(Notification notification);
    }
}
