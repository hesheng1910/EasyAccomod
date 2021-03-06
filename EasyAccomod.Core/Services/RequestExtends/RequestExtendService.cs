﻿using AGID.Core.Exceptions;
using EasyAccomod.Core.Common;
using EasyAccomod.Core.EF;
using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Enums;
using EasyAccomod.Core.Model.Post;
using EasyAccomod.Core.Model.RequestExtend;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Services.RequestExtends
{
    public class RequestExtendService : IRequestExtendService
    {
        private readonly EasyAccDbContext context;
        private readonly UserManager<AppUser> userManager;

        public RequestExtendService(EasyAccDbContext context, UserManager<AppUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<bool> CheckUserAndRole(long accessId, string role)
        {
            var user = userManager.Users.Where(x => x.Id == accessId && x.IsConfirm).FirstOrDefault();
            if (user == null) throw new ServiceException("Tài khoản không tồn tại");

            return await userManager.IsInRoleAsync(user, role);
        }
            public async Task<RequestExtend> RequestExtendPost(RequestExtendModel model)
        {
            if (await CheckUserAndRole(model.UserId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(model.UserId, CommonConstants.ADMIN) == false && await CheckUserAndRole(model.UserId, CommonConstants.OWNER) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            var post = context.Posts.Where(x => x.PostId == model.PostId && x.PostStatus == PostStatusEnum.Accepted).FirstOrDefault();
            if (post == null) throw new ServiceException("Bài đăng không tồn tại hoặc chưa được confirm");
            var user = userManager.Users.Where(u => u.Id == model.UserId && u.IsConfirm).FirstOrDefault();
            if(model.RequestTime * 7 - (DateTime.Now - post.PublicTime).Days <= 0) throw new ServiceException("Thời gian nhập nhỏ hơn thời hạn đang có của bài đăng");
            RequestExtend requestExtend = new RequestExtend()
            {
                PostId = model.PostId,
                CostOfExtend = model.RequestTime * 10000,
                RequestTime = model.RequestTime,
                RequsetExtendStatus = RequsetExtendStatusEnum.Request,
                UserName = user.UserName
            };
            Notification notification = new Notification()
            {
                Content = CommonConstants.REQUEST_EXTEND,
                NotifTime = DateTime.Now,
                OfMod = true,
                UserName = user.UserName,
                PostId = post.PostId,
                IsDelete = false
            };
            await context.Notifications.AddAsync(notification);
            await context.RequestExtends.AddAsync(requestExtend);
            await context.SaveChangesAsync();
            return requestExtend;
        }
        public async Task<List<RequestExtend>> GetPostsRequestExtend(long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false && await CheckUserAndRole(accessId, CommonConstants.OWNER) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            return context.RequestExtends.Where(x => x.RequsetExtendStatus == RequsetExtendStatusEnum.Request).ToList();
        }
        public async Task<Post> ConfirmRequestExtend(long requestId,long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            var request = context.RequestExtends.Where(x => x.Id == requestId && x.RequsetExtendStatus == RequsetExtendStatusEnum.Request).FirstOrDefault();
            if (request == null) throw new ServiceException(@"Request không tồn tại hoặc đã được accept/reject");
            var post = context.Posts.Where(x => x.PostId == request.PostId && x.IsDetele == false).FirstOrDefault();
            post.EffectiveTime +=  request.RequestTime;
            Notification notification = new Notification()
            {
                Content = CommonConstants.REQUEST_ACCEPTED,
                NotifTime = DateTime.Now,
                OfMod = false,
                UserName = request.UserName,
                PostId = post.PostId,
                IsDelete = false
            };
            await context.Notifications.AddAsync(notification);
            context.Posts.Update(post);
            request.RequsetExtendStatus = RequsetExtendStatusEnum.Accepted;
            context.RequestExtends.Update(request);
            await context.SaveChangesAsync();
            return post;
        }
        public async Task<Post> RejectRequestExtend(long requestId,long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false )
                throw new ServiceException("Tài khoản không có quyền truy cập");
            var request = context.RequestExtends.Where(x => x.Id == requestId && x.RequsetExtendStatus == RequsetExtendStatusEnum.Request).FirstOrDefault();
            if (request == null) throw new ServiceException(@"Request không tồn tại hoặc đã được accept/reject");
            var post = context.Posts.Where(x => x.PostId == request.PostId && x.IsDetele == false).FirstOrDefault();
            Notification notification = new Notification()
            {
                Content = CommonConstants.REQUEST_REJECTED,
                NotifTime = DateTime.Now,
                OfMod = false,
                UserName = request.UserName,
                PostId = post.PostId,
                IsDelete = false
            };
            await context.Notifications.AddAsync(notification);
            context.Posts.Update(post);
            request.RequsetExtendStatus = RequsetExtendStatusEnum.Rejected;
            context.RequestExtends.Update(request);
            await context.SaveChangesAsync();
            return post;
        }

        public async Task<RequestExtend> DeleteRequestExtend(long requestId,long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            var request = context.RequestExtends.Where(x => x.Id == requestId).FirstOrDefault();
            if (request == null) throw new ServiceException(@"Request không tồn tại hoặc đã được accept/reject");
            context.RequestExtends.Remove(request);
            await context.SaveChangesAsync();
            return request;
        }
    }
}
