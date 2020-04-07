using SFA.DAS.ToolService.SharedNotifications.Entities;
using SFA.DAS.ToolService.SharedNotifications.Repositories;
using SFA.DAS.ToolService.SharedNotifications.Services;
using System.Threading.Tasks;

namespace SFA.DAS.ToolService.SharedNotifications
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
