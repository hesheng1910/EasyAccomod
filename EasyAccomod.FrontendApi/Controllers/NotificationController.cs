using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAccomod.Core.Common;
using EasyAccomod.Core.Services.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyAccomod.FrontendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        [HttpGet("notifowner")]
        public async Task<IActionResult> GetNotificationForOwner()
        {
            var session = HttpContext.Session;
            var userId = session.GetString(CommonConstants.USER_SESSION);
            if (userId == null) return Unauthorized();
            var accessId = Convert.ToInt64(userId);
            return Ok(await notificationService.GetNotificationForOwner(accessId));
        }
        [HttpGet("notifmod")]
        public async Task<IActionResult> GetNotificationForMod()
        {
            var session = HttpContext.Session;
            var userId = session.GetString(CommonConstants.USER_SESSION);
            if (userId == null) return Unauthorized();
            var accessId = Convert.ToInt64(userId);
            return Ok(await notificationService.GetNotificationForMod(accessId));
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteNotifi(long notiId)
        {
            var session = HttpContext.Session;
            var userId = session.GetString(CommonConstants.USER_SESSION);
            if (userId == null) return Unauthorized();
            var accessId = Convert.ToInt64(userId);
            return Ok(await notificationService.DeleteNotification(notiId,accessId));
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var session = HttpContext.Session;
            var userId = session.GetString(CommonConstants.USER_SESSION);
            if (userId == null) return Unauthorized();
            var accessId = Convert.ToInt64(userId);
            return Ok(await notificationService.GetAll(accessId));
        }

    }
}
