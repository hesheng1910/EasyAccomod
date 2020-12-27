using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult GetNotificationForOwner()
        {
            return Ok(notificationService.GetNotificationForOwner());
        }
        [HttpGet("notifmod")]
        public IActionResult GetNotificationForMod()
        {
            return Ok(notificationService.GetNotificationForMod());
        }
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            return Ok(notificationService.GetAll());
        }
    }
}
