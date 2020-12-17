using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAccomod.Core.Services.Comments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyAccomod.BackendApi.Controllers
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
