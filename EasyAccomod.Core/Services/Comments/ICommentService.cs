using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Model.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Services.Comments
{
    public interface ICommentService
    {
        Task<Comment> AddComment(long userId, long postId, AddCommentModel model);
        Task<List<Comment>> GetCommentByPostId(long postId);
        List<Comment> GetCommentsNeedConfirm();
        Task<Comment> ConfirmComment(long cmtId);
    }
}
