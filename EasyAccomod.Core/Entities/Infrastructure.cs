using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAccomod.Core.Entities
{
    // Cơ sở vật chất
    public class Infrastructure
    {
        public long Id { get; set; }
        /// <summary>
        /// Điều hòa
        /// </summary>
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
        /// Giá điện VND/số
        /// </summary>
        public decimal ElecPrice { get; set; } 
        /// <summary>
        /// Giá nước theo m3
        /// </summary>
        public decimal WaterPrice { get; set; }
        /// <summary>
        /// Ban công
        /// </summary>
        public bool Balcony { get; set; }
        /// <summary>
        /// Phòng tắm
        /// </summary>
        public string Bath { get; set; }
        /// <summary>
        /// Bếp
        /// </summary>
        public string Kitchen { get; set; }
    }
}
