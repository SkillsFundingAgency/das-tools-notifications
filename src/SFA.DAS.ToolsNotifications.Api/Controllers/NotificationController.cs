using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ToolsNotifications.Core.Entities;
using SFA.DAS.ToolsNotifications.Core.Services;
using System.Threading.Tasks;

namespace SFA.DAS.ToolsNotifications.Api.Controllers
{
    [Route("api/[controller]")]
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
            return await _notificationService.GetNotification();
        }

        [HttpPost]
        public async Task Post(string title, string description)
        {
            await _notificationService.SetNotification(new Notification()
            {
                Title = title,
                Description = description,
                Enabled = true
            });
        }
    }
}
