using System;
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
        /// <summary>
        /// Lấy ra tài khoản theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyId")]
        public async Task<IActionResult> GetById(long id)
        {
            var session = HttpContext.Session;
            var accessId = Convert.ToInt64(session.GetString(CommonConstants.USER_SESSION));
            var result = await userService.GetById(id, accessId);
            if (result == null) BadRequest();

            return Ok(result);
        }
        /// <summary>
        /// Lấy ra danh sách người dùng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllUsers()
        {
            var session = HttpContext.Session;
            var accessId = Convert.ToInt64(session.GetString(CommonConstants.USER_SESSION));
            var result = await userService.GetAllUsers(accessId);
            if (result == null) BadRequest();
            return Ok(result);
        }
        /// <summary>
        /// Mod: Lấy các tài khoản cần confirm
        /// </summary>
        /// <returns></returns>
        [HttpGet("getconfirm")]
        public IActionResult GetUsersNeedConfirm()
        {
            var session = HttpContext.Session;
            var accessId = Convert.ToInt64(session.GetString(CommonConstants.USER_SESSION));
            return Ok(userService.GetUsersNeedConfirm(accessId));
        }
        /// <summary>
        /// Admin : Phân quyền
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("roleAssign")]
        public async Task<IActionResult> RoleAssign(RoleAssignModel model)
        {
            var session = HttpContext.Session;
            model.AccessId = Convert.ToInt64(session.GetString(CommonConstants.USER_SESSION));
            var result = await userService.RoleAssign(model);
            if (result == null) BadRequest();
            return Ok(result);
        }
        /// <summary>
        /// Mod,Admin: Cập nhật người dùng
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("updateforadmin")]
        public async Task<IActionResult> UpdateForAdmin(long userId, UserUpdateModel model)
        {
            var session = HttpContext.Session;
            model.AccessId = Convert.ToInt64(session.GetString(CommonConstants.USER_SESSION));
            var result = await userService.Update(userId, model);
            if (result == null) BadRequest();
            return Ok(result);
        }
        /// <summary>
        /// Mod: Duyệt tài khoản
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPut("confirm")]
        public async Task<IActionResult> ConfirmUser(long userId)
        {
            var session = HttpContext.Session;
            var accessId = Convert.ToInt64(session.GetString(CommonConstants.USER_SESSION));
            var result = await userService.ConfirmUser(userId, accessId);
            return Ok(result);
        }
        /// <summary>
        /// Xóa tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("deletebyid")]
        public async Task<IActionResult> Delete(long id)
        {
            var session = HttpContext.Session;
            var accessId = Convert.ToInt64(session.GetString(CommonConstants.USER_SESSION));
            var result = await userService.Delete(id, accessId);
            if (result == null) BadRequest();
            return Ok(result);
        }
    }
}
