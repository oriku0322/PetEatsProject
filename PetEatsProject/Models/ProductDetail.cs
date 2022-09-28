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
    /// 商品詳情資料
    /// </summary>
    public class ProductDetail
    {
        /// <summary>
        /// 商品詳情編號
        /// </summary>
        [Key]
        [Display(Name = "商品詳情編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 商品編號
        /// </summary>
        [Display(Name = "商品編號")]
        public int ProductsId { get; set; }
        /// 商品ForeignKey
        /// </summary>
        [ForeignKey("ProductsId")]
        [Display(Name = "商品")]
        public virtual Products Products { get; set; }

        /// <summary>
        /// 商品詳情
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "商品詳情")]
        public string Content { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime CreatDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<Cart> Carts { get; set; }
    }
}