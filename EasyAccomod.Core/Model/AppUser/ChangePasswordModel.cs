using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.AppUser
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "USERNAME_IS_REQUIRED")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "PASSWORD_IS_REQUIRED")]
        public string CurPassword { get; set; }
        [Required(ErrorMessage = "PASSWORD_IS_REQUIRED")]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        [RegularExpression("^.{8,}$", ErrorMessage = "PASSWORD_RULE_REQUIRED")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "PASSWORD_AND_CONFIRMPASSWORD_DO_NOT_MATCH")]
        public string ConfirmPassword { get; set; }
    }
}
