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
            session.SetString(CommonConstants.USER_SESSION, result.Id.ToString());
            await session.CommitAsync();
            return Ok(result);
        }
        [HttpGet("check")]
        public async Task<IActionResult> CheckAuthencate()
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString(CommonConstants.USER_SESSION));
            var userName = await userService.CheckAuthencate(userId);
            if(userName == null)
                return Ok(new { status = false,UserName = userName });
            return Ok(new { status = true, UserName = userName });
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
