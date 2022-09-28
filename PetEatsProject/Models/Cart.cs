using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PetEatsProject.Models
{
    /// <summary>
    /// 購物車暫存資訊
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// 購物車暫存資訊
        /// </summary>
        [Key]
        [Display(Name = "購物車編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 會員編號
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "會員編號")]
        public int UserId { get; set; }
        /// <summary>
        /// 會員編號ForeignKey
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        /// <summary>
        /// 商品詳情Id
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "商品詳情Id")]
        public int ProductDetailId { get; set; }
        /// <summary>
        /// 商品詳情IdForeignKey
        /// </summary>
        [ForeignKey("ProductDetailId")]
        [Display(Name = "商品詳情")]
        public virtual ProductDetail ProductDetail { get; set; }


        /// <summary>
        /// 數量
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "數量")]
        public int Amount { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [MaxLength(100)]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "備註")]
        public string Memo { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime CreatDate { get; set; }

    }
}