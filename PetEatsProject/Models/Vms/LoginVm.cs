using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetEatsProject.Models.Vms
{
    /// <summary>
    /// 登入資料
    /// </summary>
    public class LoginVm
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
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-zA-Z]).*$", ErrorMessage = "密碼格式不符")]
        public string Password { get; set; }
    }
}