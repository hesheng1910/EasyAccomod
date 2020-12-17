using EasyAccomod.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.Post
{
    public class AddPostModel
    {
        [Required(ErrorMessage = "CITY_IS_REQUIRED")]
        public string City { get; set; }
        [Required(ErrorMessage = "DISTRICT_IS_REQUIRED")]
        public string District { get; set; }
        [Required(ErrorMessage = "STREET_IS_REQUIRED")]
        public string Street { get; set; }
        public string AddressNearBy { get; set; }
        [Required(ErrorMessage = "ROOM_CATEGORY_IS_REQUIRED")]
        public string RoomCategoryName { get; set; }
        [Required(ErrorMessage = "PRICE_IS_REQUIRED")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "AREA_IS_REQUIRED")]
        public double Area { get; set; }
        [Required(ErrorMessage = "INFRASTRUCTURE_IS_REQUIRED")]
        public string Infrastructure { get; set; }
        public string Images { get; set; }
        [Required(ErrorMessage = "CONTRACT_IS_REQUIRED")]
        public string Contact { get; set; }
        public DateTime PublicTime { get; set; }
        /// <summary>
        /// Đã được thuê chưa?
        /// </summary>
        public bool Hired { get; set; }
        public IFormFile file { get; set; }
        public long UserId { get; set; }
    }
}
