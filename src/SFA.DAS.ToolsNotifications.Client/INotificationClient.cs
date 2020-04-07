using SFA.DAS.ToolService.SharedNotifications.Entities;
using System.Threading.Tasks;

namespace SFA.DAS.ToolService.SharedNotifications.Services
{
    public interface INotificationClient
    {
        Task<Notification> GetNotification();
    }
}
