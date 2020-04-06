using SFA.DAS.ToolsNotifications.Core.Entities;
using System.Threading.Tasks;

namespace SFA.DAS.ToolsNotifications.Core.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> GetNotification();

        Task SetNotification(Notification notification);
    }
}
