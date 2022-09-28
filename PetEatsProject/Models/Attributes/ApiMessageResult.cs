using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetEatsProject.Models.Attributes
{
    /// <summary>
    /// 執行結果訊息
    /// </summary>
    public class ApiMessageResult
    {
        /// <summary>
        /// 狀態
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
    }
}