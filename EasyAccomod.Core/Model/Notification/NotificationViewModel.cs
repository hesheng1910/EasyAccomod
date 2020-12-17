using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.Notification
{
    public class NotificationViewModel
    {
        public string UserName { get; set; }
        public long PostId { get; set; }
        public DateTime NotifTime { get; set; }
        public string Content { get; set; }
    }
}
