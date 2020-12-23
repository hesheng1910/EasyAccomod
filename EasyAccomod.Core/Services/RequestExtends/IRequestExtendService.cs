using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Model.Post;
using EasyAccomod.Core.Model.RequestExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Services.RequestExtends
{
    public interface IRequestExtendService
    {
        Task<RequestExtend> RequestExtendPost(RequestExtendModel model);
        List<RequestExtend> GetPostsRequestExtend();
        Task<Post> ConfirmRequestExtend(long requestId);
        Task<Post> RejectRequestExtend(long requestId);

    }
}
