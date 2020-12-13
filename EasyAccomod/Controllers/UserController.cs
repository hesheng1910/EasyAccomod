using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Model.AppUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EasyAccomod.Core.Services.User;
using EasyAccomod.Core.Common;

namespace EasyAccomod.Api.Controllers
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
        /// Đăng nhập lấy token cho vào authorize
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("login")]
        public async Task<IActionResult> Authencate(LoginModel model)
        {
            var result = await userService.Authencate(model);
            if (result == null) BadRequest();
            return Ok(result);
        }
        /// <summary>
        /// Lấy ra tài khoản theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyId")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await userService.GetById(id);
            if (result == null) BadRequest();
            return Ok(result);
        }
        /// <summary>
        /// Lấy ra danh sách người dùng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("getuserspaging")]
        public async Task<IActionResult> GetUsersPaging(GetUserPagingModel model)
        {
            var result = await userService.GetUsersPaging(model);
            if (result == null) BadRequest();
            return Ok(result);
        }
        /// <summary>
        /// Đăng ký
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register (RegisterModel model)
        {
            var result = await userService.Register(model);
            if (result == null) BadRequest();
            return Ok(result);
        }
        /// <summary>
        /// Phân quyền
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("roleAssign")]
        public async Task<IActionResult> RoleAssign(long userId, RoleAssignModel model)
        {
            var result = await userService.RoleAssign(userId,model);
            if (result == null) BadRequest();
            return Ok(result);
        }
        /// <summary>
        /// Cập nhật người dùng
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update(long userId, UserUpdateModel model)
        {
            var result = await userService.Update(userId, model);
            if (result == null) BadRequest();
            return Ok(result);
        }
        [HttpDelete("deletebyid")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await userService.Delete(id);
            if (result == null) BadRequest();
            return Ok(result);
        }
    }
}
