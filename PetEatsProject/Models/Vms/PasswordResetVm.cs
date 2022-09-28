using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetEatsProject.Models.Vms
{
    /// <summary>
    /// 重設密碼資料
    /// </summary>
    public class PasswordResetVm
    {
        /// <summary>
        /// 密碼
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-zA-Z]).*$", ErrorMessage = "密碼格式不符")]
        public string Password { get; set; }

        /// <summary>
        /// 確認密碼
        /// </summary>
        [Required(ErrorMessage = "{0}必填")]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-zA-Z]).*$", ErrorMessage = "填入密碼不同，請重新輸入")]
        [Compare("Password")]
        public string CheckPassword { get; set; }
    }
}