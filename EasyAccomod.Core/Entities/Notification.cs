using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Entities
{
    public class Notification
    {
        public long NotifId { get; set; }
        public long PostId { get; set; }
        public Post Post { get; set; }
        public DateTime NotifTime { get; set; }
        public string Content { get; set; }
        public bool IsDelete { get; set; }

    }
}
