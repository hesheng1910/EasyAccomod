using AGID.Core.Exceptions;
using EasyAccomod.Core.Common;
using EasyAccomod.Core.EF;
using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Model.Notification;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly EasyAccDbContext context;
        private readonly UserManager<AppUser> userManager;
        public NotificationService(EasyAccDbContext context, UserManager<AppUser> userManager)
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

        public async Task<Notification> DeleteNotification(long notifId,long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            var notif = await context.Notifications.FindAsync(notifId);
            if (notif == null) throw new ServiceException("Thong bao khong ton tai");
            context.Notifications.Remove(notif);
            await context.SaveChangesAsync();
            return notif;
        }

        public async Task<List<Notification>> GetNotificationForMod(long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            return context.Notifications.Where(x => x.OfMod == true).ToList();
        }
        public async Task<List<Notification>> GetNotificationForOwner(long accessId)
        {
            if (await CheckUserAndRole(accessId, CommonConstants.MODERATOR) == false && await CheckUserAndRole(accessId, CommonConstants.ADMIN) == false && await CheckUserAndRole(accessId,CommonConstants.OWNER) == false)
                throw new ServiceException("Tài khoản không có quyền truy cập");
            return context.Notifications.Where(x => x.OfMod == false).ToList();
        }
    }
}
