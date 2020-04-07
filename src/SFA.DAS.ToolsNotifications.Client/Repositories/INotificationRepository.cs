using SFA.DAS.ToolsNotifications.Client.Entities;
using System.Threading.Tasks;

namespace SFA.DAS.ToolsNotifications.Client.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> GetNotification();
    }
}
