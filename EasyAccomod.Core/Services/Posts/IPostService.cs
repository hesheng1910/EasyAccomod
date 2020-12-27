using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Enums;
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
        Task<List<PostViewModel>> GetAllPost(long accessId);
        Task<List<PostViewModel>> GetAllPostForOwner(long accessId);
        Task<PostViewModel> ViewPost(long postId);
        Task<PostViewModel> UpdatePost(long postId, long accessId, UpdatePostModel model);
        Task<Post> UpdateStatusPost(long userId, long postId, bool hired);
        Task<bool> LikePost(long postId, long userId);
        Task<List<PostViewModel>> GetFavouritePosts(long userId);
        Task<List<PostViewModel>> GetAllPostForMod(long accessId);
        Task<Post> SetPostStatus(long postId,long accessId,PostStatusEnum postStatusEnum);
        Task<List<PostViewModel>> GetMostViewPosts();
        Task<List<PostViewModel>> GetMostLikePosts();
        Task<Post> DeletePost(long postId,long accessId);
        List<PostViewModel> SearchPost(SearchPostModel model);
    }
}
