using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PetEatsProject.Models
{
    /// <summary>
    /// 訂單資訊
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 訂單資訊
        /// </summary>
        [Key]
        [Display(Name = "訂單資訊編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "商品Id")]
        public int ProductsId { get; set; }
        /// <summary>
        /// 商品IdForeignKey
        /// </summary>
        [ForeignKey("ProductsId")]
        [Display(Name = "商品")]
        public virtual Products Products { get; set; }


        /// <summary>
        /// 訂單詳細資訊Id
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "訂單詳細資訊Id")]
        public int? OrderInformationId { get; set; }
        /// <summary>
        /// 訂單詳細資訊ForeignKey
        /// </summary>
        [ForeignKey("OrderInformationId")]
        [Display(Name = "訂單詳細資訊")]
        public virtual OrderInformation OrderInformation { get; set; }


        /// <summary>
        /// 商品名稱
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "商品名稱")]
        public string ProductName { get; set; }

        /// <summary>
        /// 商品細項內容
        /// </summary>
        [MaxLength(100)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "商品細項內容")]
        public string ProductDetail { get; set; }

        /// <summary>
        /// 價格
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "價格")]
        public int Price { get; set; }

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
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "備註")]
        public string Memo { get; set; }

    }
}