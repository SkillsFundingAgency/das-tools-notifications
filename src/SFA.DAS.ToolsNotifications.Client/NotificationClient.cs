using SFA.DAS.ToolsNotifications.Client.Entities;
using SFA.DAS.ToolsNotifications.Client.Repositories;
using System.Threading.Tasks;

namespace SFA.DAS.ToolsNotifications.Client
{
    public class NotificationClient : INotificationClient
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationClient(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Notification> GetNotification()
        {
            return await _notificationRepository.GetNotification();
        }
    }
}
