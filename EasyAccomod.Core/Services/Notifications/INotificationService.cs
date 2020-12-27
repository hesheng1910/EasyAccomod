using EasyAccomod.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Services.Notifications
{
    public interface INotificationService
    {
        Task<List<Notification>> GetNotificationForMod(long accessId);
        Task<List<Notification>> GetNotificationForOwner(long accessId);
        Task<Notification> DeleteNotification(long notifId,long accessId);
    }
}
