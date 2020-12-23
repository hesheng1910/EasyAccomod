using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAccomod.Core.Common;
using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Model.Post;
using EasyAccomod.Core.Services.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace EasyAccomod.FrontendApi.Controllers
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
        /// Thêm bài đăng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddPost([FromForm]AddPostModel model)
        {
            await HttpContext.Session.LoadAsync();
            model.UserId = Convert.ToInt64(HttpContext.Session.GetString(CommonConstants.USER_SESSION));
            var result = await postService.AddPost(model);
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
        [HttpGet("getallforowner")]
        public IActionResult GetAllPostForOwner()
        {
            return Ok(postService.GetAllPostForOwner());
        }
        /// <summary>
        /// Xem bài đăng
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet("view")]
        public async Task<IActionResult> ViewPost(long postId)
        {
            var result = await postService.ViewPost(postId);
            return Ok(result);
        }
        /// <summary>
        /// Lấy danh sách user đã thích
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("getfavourite")]
        public async Task<IActionResult> GetFavouritePosts()
        {
            await HttpContext.Session.LoadAsync();
            var userId = Convert.ToInt64(HttpContext.Session.GetString(CommonConstants.USER_SESSION));
            var result = await postService.GetFavouritePosts(userId);
            return Ok(result);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdatePost(long postId,[FromForm] UpdatePostModel model)
        {
            var result = await postService.UpdatePost(postId, model);
            return Ok(result);
        }
        /// <summary>
        /// Cập nhật nhà trọ có người thuê chưa. Trả về post đó
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="hired"></param>
        /// <returns></returns>
        [HttpPut("updatestatus")]
        public async Task<IActionResult> UpdateStatusPost(long postId, bool hired)
        {
            await HttpContext.Session.LoadAsync();
            var userId = Convert.ToInt64(HttpContext.Session.GetString(CommonConstants.USER_SESSION));
            var result = await postService.UpdateStatusPost(userId,postId,hired);
            return Ok(result);
        }
        /// <summary>
        /// Like bai dang.Trả về true là like. false là dislike
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpPut("like")]
        public async Task<IActionResult> LikePost(long postId)
        {
            await HttpContext.Session.LoadAsync();
            var session = HttpContext.Session;
            var userId = Convert.ToInt64(session.GetString(CommonConstants.USER_SESSION));
            var result = await postService.LikePost(postId, userId);
            return Ok(result);
        }
        [HttpPut("reject")]
        public async Task<IActionResult> RejectPost(long postId)
        {
            var result = await postService.RejectPost(postId);
            return Ok(result);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleltePost(long postId)
        {
            var result = await postService.DeletePost(postId);
            return Ok(result);
        }
    }
}
