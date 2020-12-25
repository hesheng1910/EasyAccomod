using EasyAccomod.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.AppUser
{
    public class RoleAssignModel
    {

        public long UserId { get; set; }
        public string Role { get; set; }
        public long AccessId { get; set; }
    }
}
