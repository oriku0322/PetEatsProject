using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PetEatsProject.Models
{
    /// <summary>
    /// Token 記錄資料
    /// </summary>
    public class TokenLog
    {
        /// <summary>
        /// 編號
        /// </summary>
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 會員編號
        /// </summary>
        [Display(Name = "會員編號")]
        public int UserId { get; set; }

        //ForeignKey
        [ForeignKey("UserId")]
        [Display(Name = "會員")]
        public virtual User User { get; set; }

        /// <summary>
        /// RefreshToken
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "RefreshToken")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime CreatDate { get; set; }

        /// <summary>
        /// 過期時間
        /// </summary>
        [Display(Name = "過期時間")]
        public DateTime EndDate { get; set; }
    }
}