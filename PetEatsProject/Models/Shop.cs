using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PetEatsProject.Models
{
    /// <summary>
    /// 商店資料
    /// </summary>
    public class Shop
    {
        /// <summary>
        /// 商店編號
        /// </summary>
        [Key]
        [Display(Name = "商店編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 城市編號
        /// </summary>
        [Display(Name = "城市編號")]
        public int CityId { get; set; }
        /// <summary>
        /// 城市ForeignKey
        /// </summary>
        [ForeignKey("CityId")]
        [Display(Name = "城市")]
        public virtual City City { get; set; }

        /// <summary>
        /// 商家名稱
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "商家名稱")]
        public string ShopName { get; set; }

        /// <summary>
        /// 營業時間
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "營業時間")]
        public string OpeningHourse { get; set; }

        /// <summary>
        /// 商家電話
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "商家電話")]
        public string ShopTEL { get; set; }

        /// <summary>
        /// 商家地址
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "商家地址")]
        public string ShopAddress { get; set; }

        /// <summary>
        /// 運費
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "運費")]
        public int Freight { get; set; }

        /// <summary>
        /// 商家檔案瀏覽次數
        /// </summary>
        [Display(Name = "商家瀏覽次數")]
        public int Views { get; set; }

        /// <summary>
        /// 商家評價平均星數
        /// </summary>
        [Display(Name = "商家評價平均星數")]
        public int EvaluateStars { get; set; }

        /// <summary>
        /// 圖檔名稱
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "圖檔名稱")]
        public string Image { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime CreatDate { get; set; }


        /// <summary>
        /// 商家一對多商品
        /// </summary>
        public virtual ICollection<Products> Products { get; set; }


    }
}