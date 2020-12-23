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
        public long AddressNearById { get; set; }
        /// <summary>
        /// Loại Phòng 
        /// </summary>
        public RoomCategoryEnum RoomCategoryId { get; set; }
        /// <summary>
        /// Số phòng
        /// </summary>
        public int Rooms { get; set; }
        /// <summary>
        /// Giá tiền / tháng
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Diện tích
        /// </summary>
        public double Area { get; set; }
        /// <summary>
        /// Chung chủ hay không?
        /// </summary>
        public bool WithOwner { get; set; }
        public long InfrastructureId { get; set; }
        public string Images { get; set; }
        /// <summary>
        /// Thời gian bài viết có hiệu lực
        /// </summary>
        public DateTime ExpireTime { get; set; }
        public long TotalLike { get; set; }
        public long TotalView { get; set; }
        /// <summary>
        /// Đã được thuê chưa?
        /// </summary>
        public bool Hired { get; set; }
        public List<Report> Reports { get; set; }
        public List<Comment> Comments { get; set; }
        public AppUser AppUser { get; set; }
        public PostStatusEnum PostStatus { get; set; }
        public string Description { get; set; }
        public bool IsDetele { get; set; }
    }
}
