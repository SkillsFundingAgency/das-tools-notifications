using SFA.DAS.ToolsNotifications.Types.Entities;
using SFA.DAS.ToolsNotifications.Client.Requests;
using System.Threading.Tasks;

namespace SFA.DAS.ToolsNotifications.Client
{
    public class NotificationClient : INotificationClient
    {
        private readonly INotificationClientRequest _notificationRepository;

        public NotificationClient(INotificationClientRequest notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<Notification> GetNotification()
        {
            return await _notificationRepository.GetNotification();
        }

        public async Task SetNotification(Notification notification)
        {
            await _notificationRepository.SetNotification(notification);
        }
    }
}
