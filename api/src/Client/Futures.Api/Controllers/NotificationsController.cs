using System.Collections.Generic;
using System.Threading.Tasks;
using Futures.Notifications.Domain.Services.Notifications.Repositories;
using Futures.Notifications.Infrastructure.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Futures.Api.Controllers
{
    [Route("api/notifications")]
    public class NotificationsController : Controller
    {
        public NotificationsController(INotificationsRepository notifcations)
        {
            this.Notifcations = notifcations;
        }

        private INotificationsRepository Notifcations { get; }

        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            return this.Ok(new List<string>());
        }

        [HttpPost("{id}/read")]
        public async Task<IActionResult> ReadNotification(string id)
        {
            return this.Ok();
        }
    }
}