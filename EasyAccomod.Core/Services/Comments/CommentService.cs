using EasyAccomod.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using EasyAccomod.Core.EF;
using AGID.Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using EasyAccomod.Core.Model.Comment;
using EasyAccomod.Core.Enums;

namespace EasyAccomod.Core.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly EasyAccDbContext context;
        private readonly UserManager<AppUser> userManager;

        public CommentService(EasyAccDbContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<Comment> AddComment(long userId,long postId, AddCommentModel model)
        {
            var user = userManager.Users.Where(x => x.Id == userId && x.IsConfirm == true).FirstOrDefault();
            if (user == null) throw new ServiceException("Tài khoản không tồn tại");
            var post = context.Posts.Where(x => x.PostId == postId && x.PublicTime < DateTime.Now && x.PostStatus == PostStatusEnum.Accepted);
            if (post == null) throw new ServiceException("Bài đăng không tồn tại hoặc đã hết hạn");
            if (model.Star < 1 && model.Star > 5) throw new ServiceException("Sao phải lớn hơn hoặc bằng 1 và nhỏ hơn hoặc bằng 5");
            Comment comment = new Comment()
            {
                PostId = postId,
                UserName = user.UserName,
                Title = model.Title,
                Star = model.Star,
                ReviewContent = model.ReviewContent,
                IsConfirm = false
            };
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();

            return comment;
        }



        public async Task<List<Comment>> GetCommentByPostId(long postId)
        {
            var comments = context.Comments.Where(x => x.PostId == postId && x.IsConfirm == true).ToList();
            if (comments == null) throw new ServiceException("Không có comment nào");
            return comments;
        }

        public async Task<Comment> ConfirmComment(long cmtId)
        {
            var comment = context.Comments.Where(x => x.IsConfirm == false && x.CommentId == cmtId).FirstOrDefault();
            if (comment == null) throw new ServiceException("Comment khong ton tai");
            comment.IsConfirm = true;
            context.Comments.Update(comment);
            await context.SaveChangesAsync();
            return comment;
        }
        public List<Comment> GetCommentsNeedConfirm()
        {
            return context.Comments.Where(x => x.IsConfirm == false).ToList();
        }
    }
}
