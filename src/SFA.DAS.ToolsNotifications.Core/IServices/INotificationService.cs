using SFA.DAS.ToolsNotifications.Core.Entities;
using System.Threading.Tasks;

namespace SFA.DAS.ToolsNotifications.Core.Services
{
    public interface INotificationService
    {
        Task<Notification> GetNotification();

        Task SetNotification(Notification notification);
    }
}