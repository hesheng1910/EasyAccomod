using EasyAccomod.Core.Entities;
using EasyAccomod.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.Post
{
    public class PostViewModel
    {
        public long PostId { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        /// <summary>
        /// Phường/Xã
        /// </summary>
        public string Commune { get; set; }
        public string Street { get; set; }
        /// <summary>
        /// Địa Chỉ ở gần
        /// </summary>
        public AddressNearBy AddressNearBy { get; set; }
        public int Rooms { get; set; }
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
        /// <summary>
        /// Cở sở vật chất
        /// </summary>
        public Infrastructure Infrastructure { get; set; }
        public string Images { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// Thông tin liên hệ
        /// </summary>
        public string Contact { get; set; }
        public int EffectiveTime { get; set; }
        /// <summary>
        /// Thời gian đăng của bài viết 
        /// </summary>
        public DateTime PublicTime { get; set; }
        /// <summary>
        /// Thông tin liên lạc lấy từ bảng user
        /// </summary>
        public string FullNameOwner { get; set; }
        public string EmailOwner { get; set; }
        public long TotalLike { get; set; }
        public long TotalView { get; set; }
        public PostStatusEnum PostStatus { get; set; }
        /// <summary>
        /// Đã được thuê chưa?
        /// </summary>
        public bool Hired { get; set; }
    }
}
