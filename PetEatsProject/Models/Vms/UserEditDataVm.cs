using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetEatsProject.Models.Vms
{
    /// <summary>
    /// 會員編輯資料
    /// </summary>
    public class UserEditDataVm
    {
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
        [Display(Name = "配送地址")]
        public string Address { get; set; }
    }
}