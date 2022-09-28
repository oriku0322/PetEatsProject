using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetEatsProject.Models.Attributes
{
    /// <summary>
    /// 執行結果
    /// </summary>
    public class ApiJwtResult
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// JwtToken
        /// </summary>
        public string JwtToken { get; set; }
    }
}