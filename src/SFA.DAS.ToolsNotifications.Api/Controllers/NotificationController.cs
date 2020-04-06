using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ToolsNotifications.Core.Entities;
using SFA.DAS.ToolsNotifications.Core.Services;
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
        public async Task<ActionResult<Notification>> Get()
        {
            var notification = await _notificationService.GetNotification();

            if (notification == null)
            {
                return NotFound("No notifications have been set");
            }
            else
            {
                return Ok(notification);
            }
        }

        [HttpPost]
        public async Task Post([FromBody] Notification notification)
        {
            await _notificationService.SetNotification(notification);
        }
    }
}
