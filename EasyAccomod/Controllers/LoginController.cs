using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using EasyAccomod.Core.Common;
using EasyAccomod.Core.Model.AppUser;
using EasyAccomod.Core.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EasyAccomod.BackendApi.Controllers
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
        /// Đăng nhập
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        [HttpGet("login")]
        public async Task<IActionResult> Authencate(string userName,string password,bool rememberMe)
        {
            LoginModel model = new LoginModel()
            { UserName = userName,
              Password = password,
              RememberMe = rememberMe
            };
            var result = await userService.Authencate(model);
            var session = HttpContext.Session;
            session.SetString(CommonConstants.USER_SESSION,result.ToString());
            return Ok(result);
        }
    }
}
