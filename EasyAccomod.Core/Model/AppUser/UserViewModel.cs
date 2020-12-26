using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.AppUser
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public bool IsConfirm { get; set; }
        public IList<string> Roles { get; set; }
    }
}
