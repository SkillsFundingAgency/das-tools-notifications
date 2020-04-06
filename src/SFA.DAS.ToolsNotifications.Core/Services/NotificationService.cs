using SFA.DAS.ToolsNotifications.Core.Entities;
using SFA.DAS.ToolsNotifications.Core.Repositories;
using System.Threading.Tasks;

namespace SFA.DAS.ToolsNotifications.Core.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
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
