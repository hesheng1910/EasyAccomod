using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAccomod.Core.Common;
using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Model.Comment;
using EasyAccomod.Core.Services.Comments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace EasyAccomod.FrontendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        /// <summary>
        /// Thêm comment
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddComment(long postId, AddCommentModel model)
        {
            var session = HttpContext.Session;
            var userId = session.GetString(CommonConstants.USER_SESSION);
            if (userId == null) return Unauthorized();
            var accessId = Convert.ToInt64(userId);
            var result = await commentService.AddComment(accessId, postId, model);
            return Ok(result);
        }
        /// <summary>
        /// Lấy comment theo postId.Trả về một mảng comment
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet("getbypostid")]
        public async Task<IActionResult> GetCommentByPostId(long postId)
        {
            var session = HttpContext.Session;
            var userId = session.GetString(CommonConstants.USER_SESSION);
            if (userId == null) return Unauthorized();
            var accessId = Convert.ToInt64(userId);
            return Ok(await commentService.GetCommentByPostId(postId,accessId));
        }
        [HttpGet("getconfirm")]
        public async Task<IActionResult> GetCommentsNeedConfirm()
        {
            var session = HttpContext.Session;
            var userId = session.GetString(CommonConstants.USER_SESSION);
            if (userId == null) return Unauthorized();
            var accessId = Convert.ToInt64(userId);
            return Ok(await commentService.GetCommentsNeedConfirm(accessId));
        }
        /// <summary>
        /// Trả về comment đã confirm
        /// </summary>
        /// <param name="cmtId"></param>
        /// <returns></returns>
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmComment(long cmtId)
        {
            var session = HttpContext.Session;
            var userId = session.GetString(CommonConstants.USER_SESSION);
            if (userId == null) return Unauthorized();
            var accessId = Convert.ToInt64(userId);
            return Ok(await commentService.ConfirmComment(cmtId,accessId));
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteComment(long cmtId)
        {
            var session = HttpContext.Session;
            var userId = session.GetString(CommonConstants.USER_SESSION);
            if (userId == null) return Unauthorized();
            var accessId = Convert.ToInt64(userId);
            return Ok(await commentService.DeleteComment(cmtId,accessId));
        }
    }
}
