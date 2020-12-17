using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.AppUser
{
    public class UserUpdateModel
    {
        public long Id { get; set; }
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
        public long AccessId { get; set; }
    }
}
