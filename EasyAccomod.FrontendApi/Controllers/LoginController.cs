using EasyAccomod.Core.Common;
using EasyAccomod.Core.Model.AppUser;
using EasyAccomod.Core.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.FrontendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService userService;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }
        /// <summary>
        /// Đăng nhập .Trả về user đã được thêm vào session
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Authencate(LoginModel model)
        {
            var result = await userService.Authencate(model);
            var session = HttpContext.Session;
            session.SetString(CommonConstants.USER_SESSION, result.ToString());
            await session.CommitAsync();
            return Ok(session.GetString(CommonConstants.USER_SESSION));
        }
        [HttpGet("check")]
        public IActionResult CheckAuthencate()
        {
            if (HttpContext.Session.GetString(CommonConstants.USER_SESSION) != null)
                return Ok(true);
            return Ok(false);
        }
        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        [HttpGet("signout")]
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove(CommonConstants.USER_SESSION);
            return Ok();
        }
    }
}
