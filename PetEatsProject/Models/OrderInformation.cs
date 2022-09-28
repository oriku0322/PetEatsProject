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
    /// 訂單詳細資訊
    /// </summary>
    public class OrderInformation
    {
        /// <summary>
        /// 訂單詳細資訊
        /// </summary>
        [Key]
        [Display(Name = "訂單詳細資訊編號")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        /// 訂單狀態
        /// </summary>
        [Display(Name = "訂單狀態編號")]
        public int? OrderStatusId { get; set; }
        /// <summary>
        /// 訂單狀態編號ForeignKey
        /// </summary>
        [ForeignKey("OrderStatusId")]
        public virtual OrderStatus OrderStatus { get; set; }


        /// <summary>
        /// 付款方式編號
        /// </summary>
        [Display(Name = "付款方式編號")]
        public int? PaymentId { get; set; }
        /// <summary>
        /// 付款方式編號ForeignKey
        /// </summary>
        [ForeignKey("PaymentId")]
        public virtual Payment Payment { get; set; }


        /// <summary>
        /// 送貨編號
        /// </summary>
        [Display(Name = "送貨編號")]
        public int? ProductDeliveryId { get; set; }
        /// <summary>
        /// 送貨編號ForeignKey
        /// </summary>
        [ForeignKey("ProductDeliveryId")]
        public virtual ProductDelivery ProductDelivery { get; set; }
 

        /// <summary>
        /// 評價回饋
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "評價回饋")]
        public string Feedback { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime CreatDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}