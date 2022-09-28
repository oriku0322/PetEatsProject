using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetEatsProject.Models.Vms
{
    /// <summary>
    /// 註冊帳號
    /// </summary>
    public class SingUpVm
    {
        /// <summary>
        /// 帳號名稱
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [RegularExpression(@"[a-zA-Z0-9_]+@[a-zA-Z0-9\._]+", ErrorMessage = "{0}必填")]
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-zA-Z]).*$", ErrorMessage = "{0}必填")]
        public string Password { get; set; }

        /// <summary>
        /// 真實姓名
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "真實姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 暱稱
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "暱稱")]
        public string Nickname { get; set; }

        /// <summary>
        /// 手機號碼
        /// </summary>
        [MaxLength(50)]
        [Required(ErrorMessage = "{0}必填")]
        [RegularExpression(@"^09[0-9]{8}$", ErrorMessage = "{0}必填")]
        [Display(Name = "手機號碼")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// 配送地址
        /// </summary>
        [MaxLength(50)]
        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "配送地址")]
        public string Address { get; set; }
    }
}