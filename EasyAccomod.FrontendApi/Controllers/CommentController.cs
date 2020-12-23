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
            var userId = Convert.ToInt64(HttpContext.Session.GetString(CommonConstants.USER_SESSION));
            var result = await commentService.AddComment(userId, postId, model);
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
            return Ok(await commentService.GetCommentByPostId(postId));
        }
        [HttpGet("getconfirm")]
        public IActionResult GetCommentsNeedConfirm()
        {
            return Ok(commentService.GetCommentsNeedConfirm());
        }
        /// <summary>
        /// Trả về comment đã confirm
        /// </summary>
        /// <param name="cmtId"></param>
        /// <returns></returns>
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmComment(long cmtId)
        {
            return Ok(await commentService.ConfirmComment(cmtId));
        }
    }
}
