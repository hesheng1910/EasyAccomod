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
        /// <summary>
        /// Phường/Xã
        /// </summary>
        public string Commune { get; set; }
        [Required(ErrorMessage = "STREET_IS_REQUIRED")]
        public string Street { get; set; }
        public List<string> Educations { get; set; }
        public List<string> Medicals { get; set; }
        public List<string> BusStations { get; set; }
        [Required(ErrorMessage = "ROOMS_IS_REQUIRED")]
        public int Rooms { get; set; }
        [Required(ErrorMessage = "ROOM_CATEGORY_IS_REQUIRED")]
        public string RoomCategoryName { get; set; }
        /// <summary>
        /// Điều hòa
        /// </summary>
        [Required(ErrorMessage = "AIRCOND_IS_REQUIRED")]
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
        [Required(ErrorMessage = "ElecPrice_IS_REQUIRED")]
        public decimal ElecPrice { get; set; }
        /// <summary>
        /// Giá nước theo m3
        /// </summary>
        [Required(ErrorMessage = "WaterPrice_IS_REQUIRED")]
        public decimal WaterPrice { get; set; }
        /// <summary>
        /// Ban công
        /// </summary>
        [Required(ErrorMessage = "Balcony_IS_REQUIRED")]
        public bool Balcony { get; set; }
        /// <summary>
        /// Phòng tắm
        /// </summary>
        [Required(ErrorMessage = "BATH_IS_REQUIRED")]
        public bool Bath { get; set; }
        /// <summary>
        /// Chung chủ hay không?
        /// </summary>
        [Required(ErrorMessage = "WithOwner_IS_REQUIRED")]
        public bool WithOwner { get; set; }
        /// <summary>
        /// Bếp
        /// </summary>
        [Required(ErrorMessage = "KITCHEN_IS_REQUIRED")]
        public KitchenCategoryEnum Kitchen { get; set; }
        [Required(ErrorMessage = "PRICE_IS_REQUIRED")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "AREA_IS_REQUIRED")]
        public double Area { get; set; }
        public string Images { get; set; }
        public int EffectiveTime { get; set; }
        /// <summary>
        /// Đã được thuê chưa?
        /// </summary>
        public bool Hired { get; set; }
        public List<IFormFile> fileimgs { get; set; }
        public long UserId { get; set; }
    }
}
