using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Model.Post;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Services.Posts
{
    public interface IPostService
    {
        Task<PostViewModel> AddPost(AddPostModel model);
        List<Post> GetAllPost();
        Task<PostViewModel> ViewPost(long postId);
        Task<Post> UpdatePost(long postId, UpdatePostModel model);
        Task<Post> UpdateStatusPost(long userId, long postId, bool hired);
        Task<bool> LikePost(long postId, long userId);
        Task<List<Post>> GetFavouritePosts(long userId);
        List<Post> GetPostsNeedConfirm();
        Task<Post> ConfirmPost(long postId);
    }
}
