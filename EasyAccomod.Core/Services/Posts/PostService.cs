using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Model.Post;
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
using EasyAccomod.Core.Common;

namespace EasyAccomod.Core.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly EasyAccDbContext context;
        private readonly UserManager<AppUser> userManager;

        public PostService(EasyAccDbContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<PostViewModel> AddPost(AddPostModel model )
        {
            var filePath = @"Content/img/" + model.file.FileName;
            var stream = new FileStream(filePath, FileMode.Create);
            await model.file.CopyToAsync(stream);
            Post post = new Post()
            {
                UserId = model.UserId,
                City = model.City,
                District = model.District,
                Street = model.Street,
                AddressNearBy = model.AddressNearBy,
                Price = model.Price,
                Area = model.Area,
                Infrastructure = model.Infrastructure,
                Images = filePath,
                Hired = model.Hired,
                Contact = model.Contact,
                PublicTime = model.PublicTime,
                TotalLike = 0,
                TotalView = 0,
                IsConfirm = true
                
            };
            await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
            PostViewModel postVM = new PostViewModel()
            {
                City = model.City,
                District = model.District,
                Street = model.Street,
                AddressNearBy = model.AddressNearBy,
                Price = model.Price,
                Area = model.Area,
                Infrastructure = model.Infrastructure,
                Images = filePath,
                Hired = model.Hired,
                Contact = model.Contact,
                PublicTime = model.PublicTime,
                TotalLike = 0,
                TotalView = 0,
                IsConfirm = false
            };
            return postVM;
        }
        public List<Post> GetAllPost()
        {
            return context.Posts.ToList();
        }
        public async Task<PostViewModel> ViewPost(long postId)
        {
            var post = await context.Posts.FindAsync(postId);
            if (post == null) throw new ServiceException("Bài đăng không tồn tại");
            PostViewModel model = new PostViewModel()
            {
                City = post.City,
                District = post.District,
                Street = post.Street,
                AddressNearBy = post.AddressNearBy,
                Price = post.Price,
                Area = post.Area,
                Infrastructure = post.Infrastructure,
                Images = post.Images,
                Hired = post.Hired,
                Contact = post.Contact,
                PublicTime = post.PublicTime,
                TotalLike = post.TotalLike,
                TotalView = post.TotalView+1,
                IsConfirm = post.IsConfirm
            };
            post.TotalView += 1;
            context.Posts.Update(post);
            await context.SaveChangesAsync();
            return model;
        }

        public async Task<Post> UpdatePost(long postId, UpdatePostModel model)
        {
            var post = context.Posts.Where(x => x.PostId == postId && x.IsConfirm == false).FirstOrDefault();
            if (post == null) throw new ServiceException("Bài đăng không tồn tại hoặc đã được confirm");
            var filePath = @"Content/img/" + model.file.FileName;
            var stream = new FileStream(filePath, FileMode.Create);
            await model.file.CopyToAsync(stream);

            post.City = model.City;
            post.District = model.District;
            post.Street = model.Street;
            post.AddressNearBy = model.AddressNearBy;
            post.Price = model.Price;
            post.Area = model.Area;
            post.Infrastructure = model.Infrastructure;
            post.Images = filePath;
            post.Hired = model.Hired;
            post.Contact = model.Contact;
            post.PublicTime = model.PublicTime;

            context.Posts.Update(post);
            await context.SaveChangesAsync();
            return post;
        }
        public async Task<Post> UpdateStatusPost(long userId, long postId, bool hired)
        {
            var post = context.Posts.Where(x => x.PostId == postId && x.UserId == userId).FirstOrDefault();
            if (post == null) throw new ServiceException("Bài đăng không tồn tại");
            var user = await userManager.FindByIdAsync(userId.ToString());
            post.Hired = hired;
            context.Posts.Update(post);
            string content;
            if (hired) content = CommonConstants.HAS_HIRED;
            else content = CommonConstants.NO_TENANTS;
            Notification notification = new Notification()
            {
                UserName = user.UserName,
                PostId = postId,
                Content = content,
                NotifTime = DateTime.UtcNow,
                OfMod = true,
                IsDelete = false
            };
            await context.Notifications.AddAsync(notification);
            await context.SaveChangesAsync();
            return post;
        }
        public async Task<bool> LikePost(long postId,long userId)
        {
            var post = await context.Posts.FindAsync(postId);
            if (post == null) throw new ServiceException("Bài đăng không tồn tại");
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new ServiceException("Tài khoản không tồn tại");
            var userLikePost = context.UserLikePosts.Where(x => x.PostId == postId && x.UserId == userId).FirstOrDefault();
            if(userLikePost != null)
            {
                context.UserLikePosts.Remove(userLikePost);
                post.TotalLike -= 1;
                context.Posts.Update(post);
                await context.SaveChangesAsync();
                return false;
            }
            userLikePost = new UserLikePost()
            {
                PostId = postId,
                UserId = userId
            };
            await context.UserLikePosts.AddAsync(userLikePost);
            post.TotalLike += 1;
            context.Posts.Update(post);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Post>> GetFavouritePosts(long userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new ServiceException("Tài khoản không tồn tại");
            List<Post> posts = new List<Post>();
            var userLikePosts = context.UserLikePosts.Where(x => x.UserId == userId);
            foreach(var i in userLikePosts)
            {
                var post = await context.Posts.FindAsync(i.PostId);
                posts.Add(post);
            }
            return posts;
        }
        public async Task<Post> ConfirmPost(long postId)
        {
            var post = context.Posts.Where(x => x.IsConfirm == false && x.PostId == postId).FirstOrDefault();
            if (post == null) throw new ServiceException("Bai dang khong ton tai");
            var user = await userManager.FindByIdAsync(post.UserId.ToString());
            Notification notification = new Notification()
            {
                UserName = user.UserName,
                PostId = post.PostId,
                Content = CommonConstants.ACCEPTED,
                NotifTime = DateTime.UtcNow,
                OfMod = false,
                IsDelete = false
            };
            await context.Notifications.AddAsync(notification);
            post.IsConfirm = true;
            context.Posts.Update(post);
            await context.SaveChangesAsync();
            return post;
        }
        public List<Post> GetPostsNeedConfirm()
        {
            return context.Posts.Where(x => x.IsConfirm == false).ToList();
        }
    }
}
