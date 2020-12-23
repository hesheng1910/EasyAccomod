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
        Task<PostViewModel> AddPostForMod(AddPostModel model);
        List<PostViewModel> GetAllPost();
        List<PostViewModel> GetAllPostForOwner();
        Task<PostViewModel> ViewPost(long postId);
        Task<PostViewModel> UpdatePost(long postId, UpdatePostModel model);
        Task<Post> UpdateStatusPost(long userId, long postId, bool hired);
        Task<bool> LikePost(long postId, long userId);
        Task<List<PostViewModel>> GetFavouritePosts(long userId);
        List<PostViewModel> GetPostsNeedConfirm();
        Task<Post> ConfirmPost(long postId);
        Task<Post> RejectPost(long postId);
        Task<Post> DeletePost(long postId);
        Task<Post> RecoverRejectPost(long postId);
        List<PostViewModel> SearchPost(SearchPostModel model);
    }
}
