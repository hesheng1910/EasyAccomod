using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAccomod.Core.Common;
using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Model.Post;
using EasyAccomod.Core.Model.RequestExtend;
using EasyAccomod.Core.Services.Posts;
using EasyAccomod.Core.Services.RequestExtends;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace EasyAccomod.FrontendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestExtendController : ControllerBase
    {
        private readonly IRequestExtendService requestExtendService;

        public RequestExtendController(IRequestExtendService requestExtendService)
        {
            this.requestExtendService = requestExtendService;
        }
        [HttpPost("request")]
        public async Task<IActionResult> RequestExtendPost(RequestExtendModel model)
        {
            var session = HttpContext.Session;
            model.UserId = Convert.ToInt64(session.GetString(CommonConstants.USER_SESSION));
            var result = await requestExtendService.RequestExtendPost(model);
            return Ok(result);
        }
        [HttpGet("getrequest")]
        public IActionResult GetPostsRequestExtend()
        {
            var result = requestExtendService.GetPostsRequestExtend();
            return Ok(result);
        }
        [HttpPut("confirm")]
        public async Task<IActionResult> ConfirmRequestExtend(long requestId)
        {
            var result = await requestExtendService.ConfirmRequestExtend(requestId);
            return Ok(result);
        }
        [HttpPut("reject")]
        public async Task<IActionResult> RejectRequestExtend(long requestId)
        {
            var result = await requestExtendService.RejectRequestExtend(requestId);
            return Ok(result);
        }
    }
}
