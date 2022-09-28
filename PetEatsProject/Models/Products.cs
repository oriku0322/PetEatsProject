using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PetEatsProject.Models
{
    /// <summary>
    /// 商品資料
    /// </summary>
    public class Products
    {
        /// <summary>
        /// 商品編號
        /// </summary>
        [Key]
        [Display(Name = "商品編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 商家編號
        /// </summary>
        [Display(Name = "商家編號")]
        public int ShopId { get; set; }
        /// 商家ForeignKey
        /// </summary>
        [ForeignKey("ShopId")]
        [Display(Name = "商家")]
        public virtual Shop Shop { get; set; }


        /// <summary>
        /// 品項分類編號
        /// </summary>
        [Display(Name = "品項分類編號")]
        public int ProductClassId { get; set; }
        /// 品項分類ForeignKey
        /// </summary>
        [ForeignKey("ProductClassId")]
        [Display(Name = "品項分類")]
        public virtual ProductClass ProductClass { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "商品名稱")]
        public string ProductName { get; set; }

        /// <summary>
        /// 價格
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "價格")]
        public int Price { get; set; }

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
        /// 商品一對多商品詳情
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }

        //[JsonIgnore]
        //public virtual ICollection<Cart> Carts { get; set; }

    }
}