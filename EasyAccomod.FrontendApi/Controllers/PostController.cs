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
        [HttpPost("search")]
        public IActionResult SearchPost(SearchPostModel model)
        {
            var result = postService.SearchPost(model);
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
        /// <summary>
        /// Mod: Từ chối bài đăng
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpPut("reject")]
        public async Task<IActionResult> RejectPost(long postId)
        {
            var result = await postService.RejectPost(postId);
            return Ok(result);
        }
        /// <summary>
        /// Mod: Xóa những bài đăng request
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleltePost(long postId)
        {
            var result = await postService.DeletePost(postId);
            return Ok(result);
        }
        /// <summary>
        /// Mod: Thêm bài đăng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("addformod")]
        public async Task<IActionResult> AddPostForMod([FromForm] AddPostModel model)
        {
            var session = HttpContext.Session;
            var userId = session.GetString(CommonConstants.USER_SESSION);
            if (userId == null) return Unauthorized();
            model.UserId = Convert.ToInt64(userId);
            var result = await postService.AddPostForMod(model);
            if (result == null) return BadRequest();
            return Ok(result);
        }
        /// <summary>
        /// Mod: Lấy những post cần confirm
        /// </summary>
        /// <returns></returns>
        [HttpGet("gettoconfirm")]
        public IActionResult GetPostsNeedConfirm()
        {
            var result = postService.GetPostsNeedConfirm();
            return Ok(result);
        }
        /// <summary>
        /// Mod: Confirm 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpPut("confirm")]
        public async Task<IActionResult> ConfirmPost(long postId)
        {
            var result = await postService.ConfirmPost(postId);
            return Ok(result);
        }
        /// <summary>
        /// Mod: Accepted lại các post đã reject
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpPut("recover")]
        public async Task<IActionResult> RecoverRejectedPost(long postId)
        {
            var result = await postService.RecoverRejectPost(postId);
            return Ok(result);
        }
    }
}
