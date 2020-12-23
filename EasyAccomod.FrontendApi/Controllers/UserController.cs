﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAccomod.Core.Common;
using EasyAccomod.Core.Model.AppUser;
using EasyAccomod.Core.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyAccomod.FrontendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
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
        /// <summary>
        /// Đăng ký làm chủ trọ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await userService.Register(model);
            if (result == null) BadRequest();
            return Ok(result);
        }
        /// <summary>
        /// Đăng ký renter
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("registerrenter")]
        public async Task<IActionResult> RegisterForRenter(RegisterModel model)
        {
            var result = await userService.RegisterForRenter(model);
            if (result == null) BadRequest();
            return Ok(result);
        }
        /// <summary>
        /// Cập nhật thông tin
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update(long userId, UserUpdateModel model)
        {
            var session = HttpContext.Session;
            model.AccessId = Convert.ToInt64(session.GetString(CommonConstants.USER_SESSION));
            var result = await userService.Update(userId, model);
            if (result == null) BadRequest();
            return Ok(result);
        }
    }
}
