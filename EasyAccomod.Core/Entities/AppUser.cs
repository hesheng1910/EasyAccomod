using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EasyAccomod.Core.Entities
{
    public class AppUser : IdentityUser<long>
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Address { get; set; }
        public List<Notification> Notifications { get; set; }
        public bool IsConfirm { get; set; }

    }
}
