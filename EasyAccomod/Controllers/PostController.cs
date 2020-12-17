using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAccomod.Core.Common;
using EasyAccomod.Core.Model.Post;
using EasyAccomod.Core.Services.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyAccomod.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }
        /// <summary>
        /// Mod: Thêm bài đăng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddPost([FromForm]AddPostModel model)
        {
            var session = HttpContext.Session;
            var userId = session.GetString(CommonConstants.USER_SESSION);
            if (userId == null) return Unauthorized();
            model.UserId = Convert.ToInt64(userId);
            var result = await postService.AddPost(model);
            if (result == null) return BadRequest();
            return Ok(result);
        }
        /// <summary>
        /// Lấy tất cả bài đăng
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IActionResult GetAllPost()
        {
            return Ok(postService.GetAllPost());
        }
        [HttpGet("gettoconfirm")]
        public IActionResult GetPostsNeedConfirm()
        {
            var result = postService.GetPostsNeedConfirm();
            return Ok(result);
        }
        [HttpPut("confirm")]
        public async Task<IActionResult> ConfirmPost(long postId)
        {
            var result = await postService.ConfirmPost(postId);
            return Ok(result);
        }
    }
}
