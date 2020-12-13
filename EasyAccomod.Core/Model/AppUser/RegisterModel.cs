using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.AppUser
{
    public class RegisterModel
    {
        /// <summary>
        /// Tên đăng nhập ít nhất 6 kí tự
        /// </summary>
        [Required(ErrorMessage = "USERNAME_IS_REQUIRED")]
        [RegularExpression("^.{6,}$", ErrorMessage = "USERNAME_RULE_REQUIRED")]
        public string UserName { get; set; }
        /// <summary>
        /// Mật khẩu dài ít nhất 8 ký tự bất kỳ
        /// </summary>
        [Required(ErrorMessage = "PASSWORD_IS_REQUIRED")]
        [MaxLength(200)]
        [DataType(DataType.Password)]
        [RegularExpression("^.{8,}$", ErrorMessage = "PASSWORD_RULE_REQUIRED")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "PASSWORD_AND_CONFIRMPASSWORD_DO_NOT_MATCH")]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        [Required(ErrorMessage = "FIRST_NAME_IS_REQUIRED")]
        public string FirstName { get; set; }
        /// <summary>
        /// Họ
        /// </summary>
        [Required(ErrorMessage = "LAST_NAME_IS_REQUIRED")]
        public string LastName { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Required(ErrorMessage = "PHONE_IS_REQUIRED")]
        public string Phone { get; set; }
        /// <summary>
        /// Địa chỉ email
        /// </summary>
        [Required(ErrorMessage = "EMAIL_IS_REQUIRED")]
        [EmailAddress(ErrorMessage = "INVALID_EMAIL_ADDRESS)")]
        public string Email { get; set; }
        /// <summary>
        /// Địa chỉ thường trú
        /// </summary>
        [Required(ErrorMessage = "ADDRESS_IS_REQUIRED")]
        public string Address { get; set; }
        /// <summary>
        /// Số CMND
        /// </summary>
        [Required(ErrorMessage = "IDENTITY_NUMBER_IS_REQUIRED")]
        public string IdentityNumber { get; set; }
    }
}
