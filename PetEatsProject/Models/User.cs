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
    /// 會員資料
    /// </summary>
    public class User
    {
        /// <summary>
        /// 會員編號
        /// </summary>
        [Key]
        [Display(Name = "會員編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 信箱帳號
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "信箱帳號")]
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "密碼")]
        public string HashPassword { get; set; }

        /// <summary>
        /// 雜湊鹽
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "密碼鹽")]
        public string Salt { get; set; }

        /// <summary>
        /// 真實姓名
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "真實姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 暱稱
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "暱稱")]
        public string Nickname { get; set; }

        /// <summary>
        /// 手機號碼
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(50)]
        [Display(Name = "手機號碼")]
        public string MobilePhone { get; set; }


        /// <summary>
        /// 配送地址
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [MaxLength(100)]
        [Display(Name = "配送地址")]
        public string Address { get; set; }

        /// <summary>
        /// 大頭貼
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "大頭貼")]
        public string UserImage { get; set; }


        /// <summary>
        /// 帳號開通
        /// </summary>
        [Display(Name = "帳號開通")]
        public bool AccountState { get; set; }


        /// <summary>
        /// 建立時間
        /// </summary>
        [Display(Name = "建立時間")]
        public DateTime? CreatDate { get; set; }

        /// <summary>
        /// 信箱驗證碼
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "信箱驗證碼")]
        public string CheckMailCode { get; set; }

        /// <summary>
        /// 信箱驗證碼到期時間
        /// </summary>
        [Display(Name = "信箱驗證碼到期時間")]
        public DateTime MailCodeCreatDate { get; set; }

        /// <summary>
        /// RefreshToken
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "RefreshToken")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// RefreshToken建立時間
        /// </summary>
        [Display(Name = "RefreshToken建立時間")]
        public DateTime? RefreshTokenCreatDate { get; set; }

        /// <summary>
        /// /JI預防迴圈
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<TokenLog> TokenLogs { get; set; }
        [JsonIgnore]
        public virtual ICollection<OrderInformation> OrderInformation { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }

    }
}