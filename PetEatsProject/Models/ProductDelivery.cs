using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PetEatsProject.Models
{
    /// <summary>
    /// 送貨方式
    /// </summary>
    public class ProductDelivery
    {
        /// <summary>
        /// 送貨編號
        /// </summary>
        [Key]
        [Display(Name = "送貨編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 送貨方式
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "送貨方式")]
        public string ProductDeliveryName { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime CreatDate { get; set; }

        public virtual ICollection<OrderInformation> OrderInformation { get; set; }
    }
}