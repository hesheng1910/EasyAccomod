using EasyAccomod.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.AppUser
{
    public class RoleAssignModel
    {
        public Guid UserId { get; set; }
        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
    }
}
