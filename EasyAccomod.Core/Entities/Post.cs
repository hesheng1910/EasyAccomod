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
        public long PostId { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string AddressNearBy { get; set; }
        public RoomCategoryEnum RoomCategory { get; set; }
        public decimal Price { get; set; }
        public double Area { get; set; }
        /// <summary>
        /// Chung chủ Hay không?
        /// </summary>
        public bool IsOwner { get; set; }
        public string Property { get; set; }
        public byte[] Images { get; set; }
        public string Contact { get; set; }
        public DateTime PublicTime { get; set; }
        public long TotalLike { get; set; }
        public long TotalView { get; set; }
        /// <summary>
        /// Đã được thuê chưa?
        /// </summary>
        public bool Hired { get; set; }
        public List<Report> Reports { get; set; }
        public List<Comment> Comments { get; set; }
        public bool IsConfirm { get; set; }
    }
}
