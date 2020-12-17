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

        public List<Notification> GetNotificationForMod()
        {
            return context.Notifications.Where(x => x.OfMod == true).ToList();
        }
        public List<Notification> GetNotificationForOwner()
        {
            return context.Notifications.Where(x => x.OfMod == false).ToList();
        }
    }
}
