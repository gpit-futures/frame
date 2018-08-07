using System;
using System.Linq;
using System.Threading.Tasks;
using Futures.Notifications.Domain.Services.Notifications.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Futures.Api.Controllers
{
    [Authorize(Policy = "Read")]
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
            var ods = this.User.FindFirst(x => x.Type == "odsId");

            if (ods == null)
            {
                return this.BadRequest();
            }

            var user = this.User.FindFirst(x => x.Type == "user_name");

            var notifications = await this.Notifcations.GetAllByOds(ods.Value, user.Value);
            return this.Ok(notifications.OrderByDescending(x => x.DateCreated));
        }

        [HttpPost("{id}/read")]
        public async Task<IActionResult> ReadNotification(Guid id)
        {
            var ods = this.User.FindFirst(x => x.Type == "odsId");

            if (ods == null)
            {
                return this.BadRequest();
            }

            var user = this.User.FindFirst(x => x.Type == "user_name");

            var notification = await this.Notifcations.GetOne(id);

            if (notification == null)
            {
                return this.NotFound();
            }

            var readKey = $"{ods.Value}:{user.Value}";

            if (!notification.Read.Contains(readKey))
            {
                notification.Read = notification.Read.Append(readKey);

                await this.Notifcations.AddOrUpdate(notification);
            }

            return this.Ok();
        }
    }
}