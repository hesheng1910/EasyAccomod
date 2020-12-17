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
        /// <summary>
        /// SelectItem để chứa result của check box
        /// </summary>
        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
        public long AccessId { get; set; }
    }
}
