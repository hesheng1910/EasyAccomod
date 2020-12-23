using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.AppUser
{
    /// <summary>
    /// Model đăng nhập
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        [Required(ErrorMessage = "USER_NAME_IS_REQUIRED")]
        public string UserName { get; set; }
        /// <summary>
        /// Mật Khẩu
        /// </summary>
        [Required(ErrorMessage = "PASSWORD_IS_REQUIRED")]
        public string Password { get; set; }
    }
}
