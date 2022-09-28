using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PetEatsProject.Models
{
    /// <summary>
    /// 訂單狀態
    /// </summary>
    public class OrderStatus
    {
        /// <summary>
        /// 訂單狀態編號
        /// </summary>
        [Key]
        [Display(Name = "訂單狀態編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 訂單狀態
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "訂單狀態")]
        public string OrderStatusName { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime CreatDate { get; set; }

        public virtual ICollection<OrderInformation> OrderInformation { get; set; }

    }
}