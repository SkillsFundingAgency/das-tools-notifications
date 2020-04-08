using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ToolsNotifications.Api.Models;
using SFA.DAS.ToolsNotifications.Core.IServices;
using SFA.DAS.ToolsNotifications.Core.Services;
using SFA.DAS.ToolsNotifications.Types.Entities;
using System.Threading.Tasks;

namespace SFA.DAS.ToolsNotifications.Api.Controllers
{
    [Route("")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<ActionResult<NotificationDto>> Get()
        {
            var notification = await _notificationService.GetNotification();

            if (notification == null)
            {
                return NotFound("No notifications have been set");
            }
            else
            {
                return Ok(new NotificationDto
                {
                    Title = notification.Title,
                    Description = notification.Description,
                    Enabled = notification.Enabled
                });
            }
        }

        [HttpPost]
        public async Task Post([FromBody] NotificationDto notification)
        {
            await _notificationService.SetNotification(new Notification
            {
                Title = notification.Title,
                Description = notification.Description,
                Enabled = notification.Enabled
            });
        }
    }
}
