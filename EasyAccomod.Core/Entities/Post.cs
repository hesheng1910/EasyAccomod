using EasyAccomod.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Entities
{
    public class Post
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        /// <summary>
        /// Địa Chỉ ở gần
        /// </summary>
        public string AddressNearBy { get; set; }
        /// <summary>
        /// Loại Phòng 
        /// </summary>
        public RoomCategoryEnum RoomCategoryId { get; set; }
        /// <summary>
        /// Giá tiền / tháng
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Diện tích
        /// </summary>
        public double Area { get; set; }
        public string Infrastructure { get; set; }
        public string Images { get; set; }
        /// <summary>
        /// Thông tin liên hệ
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// Thời gian bài viết có hiệu lực
        /// </summary>
        public DateTime PublicTime { get; set; }
        public long TotalLike { get; set; }
        public long TotalView { get; set; }
        /// <summary>
        /// Đã được thuê chưa?
        /// </summary>
        public bool Hired { get; set; }
        public List<Report> Reports { get; set; }
        public List<Comment> Comments { get; set; }
        public AppUser AppUser { get; set; }
        public bool IsConfirm { get; set; }
    }
}
