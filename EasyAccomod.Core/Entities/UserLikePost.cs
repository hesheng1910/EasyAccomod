using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Entities
{
    public class UserLikePost
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public AppUser User { get; set; }
        public Post Post { get; set; }
    }
}
