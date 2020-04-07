using SFA.DAS.ToolService.SharedNotifications.Entities;
using System.Threading.Tasks;

namespace SFA.DAS.ToolService.SharedNotifications.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> GetNotification();
    }
}
