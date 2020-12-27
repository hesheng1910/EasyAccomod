using EasyAccomod.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Model.Post
{
    public class SearchPostModel
    {
        public string City { get; set; }
        public string District { get; set; }
        public string Commune { get; set; }
        public string Street { get; set; }
        public string AddressNearBy { get; set; }
        public int RoomCategoryId { get; set; }
        public bool AirCond { get; set; }
        /// <summary>
        /// Tủ lạnh
        /// </summary>
        public bool Fridge { get; set; }
        /// <summary>
        /// Bình nước nóng
        /// </summary>
        public bool WaterHeater { get; set; }
        /// <summary>
        /// Phòng tắm
        /// </summary>
        public bool Bath { get; set; }
        /// <summary>
        /// Chung chủ hay không?
        /// </summary>
        public bool WithOwner { get; set; }
        /// <summary>
        /// Bếp
        /// </summary>
        public int Kitchen { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
    }
}
