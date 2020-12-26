using EasyAccomod.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Services.Notifications
{
    public interface INotificationService
    {
        List<Notification> GetNotificationForMod();
        List<Notification> GetNotificationForOwner();
        Task<Notification> DeleteNotification(long notifId);
    }
}
