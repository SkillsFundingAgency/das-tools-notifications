﻿using SFA.DAS.ToolsNotifications.Types.Entities;
using System.Threading.Tasks;

namespace SFA.DAS.ToolsNotifications.Core.IServices
{
    public interface INotificationService
    {
        Task<Notification> GetNotification();

        Task SetNotification(Notification notification);
    }
}
