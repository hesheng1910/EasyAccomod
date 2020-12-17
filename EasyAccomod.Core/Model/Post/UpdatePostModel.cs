using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.Post
{
    public class UpdatePostModel
    {
        [Required(ErrorMessage = "CITY_IS_REQUIRED")]
        public string City { get; set; }
        [Required(ErrorMessage = "DISTRICT_IS_REQUIRED")]
        public string District { get; set; }
        [Required(ErrorMessage = "STREET_IS_REQUIRED")]
        public string Street { get; set; }
        public string AddressNearBy { get; set; }
        /// <summary>
        /// Loại phòng được đánh số 1,2,3,4 chi tiết trong bảng RoomCategory
        /// </summary>
        [Required(ErrorMessage = "ROOM_CATEGORY_IS_REQUIRED")]
        public string RoomCategoryName { get; set; }
        [Required(ErrorMessage = "PRICE_IS_REQUIRED")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "AREA_IS_REQUIRED")]
        public double Area { get; set; }
        /// <summary>
        /// Cơ sở vật chất
        /// </summary>
        [Required(ErrorMessage = "INFRASTRUCTURE_IS_REQUIRED")]
        public string Infrastructure { get; set; }
        /// <summary>
        /// Nhiều link ảnh cách nhau bằng dấu ;
        /// </summary>
        public string Images { get; set; }
        [Required(ErrorMessage = "CONTRACT_IS_REQUIRED")]
        public string Contact { get; set; }
        /// <summary>
        /// Thời gian bài đăng có hiệu lực
        /// </summary>
        public DateTime PublicTime { get; set; }
        /// <summary>
        /// Đã được thuê chưa?
        /// </summary>
        public bool Hired { get; set; }
        /// <summary>
        /// Thêm file
        /// </summary>
        public IFormFile file { get; set; }
    }
}
