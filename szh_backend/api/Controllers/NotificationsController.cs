using Microsoft.AspNetCore.Mvc;
using szh.cultivation.notifications;
using szh.dao;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class NotificationsController : Controller {

        // GET api/Notifications
        [HttpGet]
        public IActionResult Get() {
            return new ObjectResult(Notification.GetNotifications());
        }

        [HttpPost]
        public IActionResult CreateNotification([FromBody] NotificationAddModel notification) {
            if (notification.condition == null || notification.measurement_type == null || notification.receivers == "" ||
                notification.repeat_after == 0 || notification.tunnel == null || notification.value == 0) {
                return BadRequest();
            } else {
                if (Notification.AddNotification(notification)) {
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}