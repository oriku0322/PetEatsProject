using NSwag.Annotations;
using PetEatsProject.Models;
using PetEatsProject.Models.Attributes;
using PetEatsProject.Models.Vms;
using PetEatsProject.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PetEatsProject.Controllers
{
    /// <summary>
    /// 使用者操作功能
    /// </summary>
    [OpenApiTag("User", Description = "使用者操作功能")]

    public class UserController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //// Get: api/users/hello
        ///// <summary>
        ///// 測試
        ///// </summary>
        ///// <param name="userData">註冊資料</param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("api/users/hello")]
        //public IHttpActionResult Text()
        //{

        //    return Ok(new { Status = "success", Message = "HELLO" });

        //}

        //// Get: api/users/text
        ///// <summary>
        ///// 測試
        ///// </summary>
        ///// <param name="userData">註冊資料</param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("api/users/text")]
        //public IHttpActionResult Text(int id)
        //{
        //    // 查詢指定帳號
        //    var userQuery = db.Users.FirstOrDefault(x => x.Id == id);

        //    return Ok(new { Status = "success", Message = userQuery.Account });

        //}


        // POST: api/users/sign-up
        /// <summary>
        /// 1-1 會員註冊 + 驗證發信
        /// </summary>
        /// <param name="userData">註冊資料</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/users/sign-up")]
        public IHttpActionResult SignUp(SingUpVm userData)
        {
            // 必填欄位資料檢查
            if (!ModelState.IsValid) return Ok(new { Status = false, Message = "欄位資料不符" });
            if (userData == null) return Ok(new { Status = false, Message = "未填欄位" });
            // 帳號已註冊過
            if (!(db.Users.FirstOrDefault(x => x.Account == userData.Account) == null)) return Ok(new { Status = false, Message = "帳號已存在" });

            try
            {
                // 生成密碼雜湊鹽
                string saltStr = HashPassword.CreateSalt();
                // 生成郵件連結驗證碼
                string mailGuid = Guid.NewGuid().ToString();
                //// 生成使用者資料
                //string[] accountArr = userData.Account.Split('@');

                User userInput = new User
                {
                    Account = userData.Account,
                    HashPassword = HashPassword.GenerateHashWithSalt(userData.Password, saltStr),
                    Salt = saltStr,
                    Name = userData.Name,
                    Nickname = userData.Nickname,
                    MobilePhone = userData.MobilePhone,
                    Address = userData.Address,
                    AccountState = false,
                    CreatDate = DateTime.Now,
                    CheckMailCode = mailGuid,
                    MailCodeCreatDate = DateTime.Now.AddDays(1),// 設定驗證碼效期1天
                };
                // 加入資料並儲存
                db.Users.Add(userInput);
                db.SaveChanges();

                string verifyLink = Mail.SetAuthMailLink(Request.RequestUri.Host, mailGuid);
                Mail.SendVerifyLinkMail(userData.Name, userData.Account, verifyLink);
            }
            catch (Exception ex)
            {
                return Ok(new { Status = false, ex.Message });
            }
            return Ok(new { Status = true, Message = "註冊成功，請至信箱點選開通帳號連結登入!" });
        }

        /// <summary>
        /// 1-2 註冊開通
        /// </summary>
        /// <param name="guid">信箱驗證資料</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/users/auth-mail/")]
        public IHttpActionResult AuthMail(string guid)
        {
            // 查詢指定帳號
            var userQuery = db.Users.FirstOrDefault(x => x.CheckMailCode == guid);

            if (guid == null) return Ok(new { Status = false, Message = "未填欄位" });
            // 帳號檢查
            else if (userQuery == null) return Ok(new { Status = false, Message = "帳號驗證碼不存在" });

            // 判斷 guid 時效及開通狀態，驗證碼期限未到期就開通帳號
            string verifyLink;
            string[] accountArr;
            if (userQuery.AccountState)
            {
                // 帳號已開通
                return Ok(new { Status = false, Message = "帳號已經開通過，請直接登入會員" });
            }
            else if (userQuery.MailCodeCreatDate < DateTime.Now && !userQuery.AccountState)
            {
                // 驗證連結 guid 過期，生成郵件連結驗證碼重寄驗證信
                string mailGuid = Guid.NewGuid().ToString();
                userQuery.CheckMailCode = mailGuid;
                userQuery.MailCodeCreatDate = DateTime.Now.AddDays(1);
                verifyLink = Mail.SetAuthMailLink(Request.RequestUri.Host, mailGuid);
                accountArr = userQuery.Account.Split('@');
                Mail.SendVerifyLinkMail(accountArr[0], userQuery.Account, verifyLink);
                return Ok(new { Status = false, Message = "開通驗證連結過期，已重新發信，請至信箱收信重新驗證" });
            }

            // 開通更新
            userQuery.AccountState = true;
            db.SaveChanges();

            return Ok(new { Status = true, Message = "帳號開通成功，請重新登入!" });
        }

        /// <summary>
        /// 1-3 會員登入
        /// </summary>
        /// <param name="userData">登入資料</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/users/login")]
        public IHttpActionResult Login(LoginVm userData)
        {
            // 查詢指定帳號
            var userQuery = db.Users.FirstOrDefault(x => x.Account == userData.Account);

            // 必填欄位資料檢查
            if (!ModelState.IsValid) return Ok(new { Status = false, Message = "未填欄位" });
            if (userData == null) return Ok(new { Status = false, Message = "未填欄位" });
            // 帳號檢查
            else if (userQuery == null) return Ok(new { Status = false, Message = "帳號不存在" });

            // 登入密碼加鹽雜湊結果
            string hashPassword = HashPassword.GenerateHashWithSalt(userData.Password, userQuery.Salt);
            // 密碼檢查
            if (!(userQuery.HashPassword.Equals(hashPassword))) return Ok(new { Status = false, Message = "密碼不正確" });

            // 有網址傳值 guid 且驗證碼期限未到期就開通帳號
            string verifyLink;
            string[] accountArr;
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.GenerateToken(userQuery.Id);
            if (!userQuery.AccountState && userQuery.MailCodeCreatDate > DateTime.Now)
            {
                // 權限未開通且信箱開通連結未過期
                return Ok(new { Status = false, Message = "未開通驗證，請至信箱收信開通驗證" });
            }
            else if (!userQuery.AccountState)
            {
                // 權限未開通且信箱開通連結已過期，生成郵件連結驗證碼重寄驗證信
                string mailGuid = Guid.NewGuid().ToString();
                userQuery.CheckMailCode = mailGuid;
                userQuery.MailCodeCreatDate = DateTime.Now.AddDays(1);
                db.SaveChanges();
                verifyLink = Mail.SetAuthMailLink(Request.RequestUri.Host, mailGuid);
                accountArr = userData.Account.Split('@');
                Mail.SendVerifyLinkMail(accountArr[0], userData.Account, verifyLink);
                return Ok(new { Status = false, Message = "開通驗證連結過期，已重新發信，請至信箱收信重新驗證" });
            }

            // 一般登入
            return Ok(new ApiJwtResult { Status = true, JwtToken = jwtToken });
        }

        /// <summary>
        /// 1-4 檢視會員資料
        /// </summary>
        /// <param name="id">會員ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/users/info")]
        [JwtAuthFilter]
        public IHttpActionResult GetUserInfo()
        {
            var data = JwtAuthUtil.GetUserList(Request.Headers.Authorization.Parameter);
            var account = data.Item2;
            var user = db.Users.FirstOrDefault(x => x.Account == account);
            var result = new
            {
                user.Account,
                user.Name,
                user.Nickname,
                user.MobilePhone,
                user.Address,
            };
            return Ok(new { status = true, message = "會員資料", userdata = result });
        }

        /// <summary>
        /// 1-5 修改會員資料
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPut]
        [Route("api/users/edit")]
        public IHttpActionResult EditUserInfo(UserEditDataVm userData)
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            // 檢查欄位
            if (userData == null) return Ok(new { Status = false, JwtToken = jwtToken, Message = "未填欄位" });

            //// 查詢指定活動收藏記錄
            var userQuery = db.Users.FirstOrDefault(x => x.Id == userId);
            // 設定更新值
            userQuery.Name = userData.Name;
            userQuery.Nickname = userData.Nickname;
            userQuery.MobilePhone = userData.MobilePhone;
            userQuery.Address = userData.Address;

            db.SaveChanges();

            return Ok(new { Status = true, JwtToken = jwtToken, Message = "編輯內容更新成功" });
        }

        /// <summary>
        /// 1-6 登入時重設密碼(JWT)
        /// </summary>
        /// <param name="resetDate">重設密碼</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPost]
        [Route("api/users/login-reset-password")]
        public IHttpActionResult LoginResetPassword(PasswordResetVm resetDate)
        {
            //必填檢查資料欄位
            if (!ModelState.IsValid) return Ok(new { Status = false, Message = "未填欄位" });
            if (resetDate == null) return Ok(new { Status = false, Message = "未填欄位" });

            //解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            //單純刷 新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            // 查詢指定指定帳號
            var userQuery = db.Users.FirstOrDefault(x => x.Id == userId);
            //生成密碼鹽
            string saltStr = HashPassword.CreateSalt();
            //登入密碼鹽結果
            string hashPassword = HashPassword.GenerateHashWithSalt(resetDate.Password, saltStr);

            //存入新密碼
            userQuery.Salt = saltStr;
            userQuery.HashPassword = hashPassword;
            db.SaveChanges();

            return Ok(new { Status = true, JwtToken = jwtToken, Message = "密碼更新成功" });
        }

        /// <summary>
        /// 1-7 檢查會員TOKEN登入狀態
        /// </summary>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPost]
        [Route("api/users/check")]
        public IHttpActionResult GetUserRealData()
        {
            // 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            //單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            var idData = (int)userToken["Id"];

            // 查詢指定帳號
            var userQuery = db.Users.AsNoTracking().FirstOrDefault(x => x.Id == idData);

            // 回傳 JwtToken 及姓名+手機
            return Ok(new { Status = true, Message = "您已登入成功" });
        }

        /// <summary>
        /// 1-8 忘記密碼發信
        /// </summary>
        /// <param name="accountData">帳號資料</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/users/forget-password-mail")]
        public IHttpActionResult SendResetPasswordMail(AccountVm accountData)
        {
            // 查詢指定帳號
            var userQuery = db.Users.FirstOrDefault(x => x.Account == accountData.Account);

            if (accountData.Account == null) return Ok(new { Status = false, Message = "未填欄位" });
            // 帳號檢查
            else if (userQuery == null) return Ok(new { Status = false, Message = "帳號不存在" });
            else if (!Mail.RegexEmail(accountData.Account)) return Ok(new { Status = false, Message = "信箱格式不符" });

            // 生成重設密碼連結，發信
            string resetLink;
            string[] accountArr;
            string mailGuid = Guid.NewGuid().ToString();
            userQuery.CheckMailCode = mailGuid;
            userQuery.MailCodeCreatDate = DateTime.Now;
            resetLink = Mail.SetResetPasswordMailLink(Request.RequestUri.Host, mailGuid);
            accountArr = userQuery.Account.Split('@');
            Mail.SendResetLinkMail(accountArr[0], userQuery.Account, resetLink);
            db.SaveChanges();

            return Ok(new { Status = true, Message = "已發送重設密碼連結至信箱，請至信箱收信點選連結重設密碼" });
        }

        /// <summary>
        /// 1-9 信箱連入重設密碼
        /// </summary>
        /// <param name="resetData">信箱連入重設密碼資料</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/users/mail-reset-password")]
        public IHttpActionResult MailLinkResetPassword(PasswordMailResetVm resetData)
        {
            // 查詢指定帳號
            var userQuery = db.Users.FirstOrDefault(x => x.CheckMailCode == resetData.Guid);

            if (resetData.Guid == null) return Ok(new { Status = false, Message = "未填欄位" });
            //else if (userQuery.MailCodeCreatDate.AddMinutes(30) > DateTime.Now) return Ok(new ApiMessageResult { Status = false, Message = "連結驗證碼超過30分鐘" });
            // 帳號檢查
            else if (userQuery == null) return Ok(new { Status = false, Message = "連結驗證碼不存在" });
            // 密碼格式不符
            if (String.IsNullOrEmpty(resetData.Password) || String.IsNullOrEmpty(resetData.CheckPassword)) return Ok(new { Status = false, Message = "密碼未填" });
            else if (!Mail.RegexPassword(resetData.Password) || !Mail.RegexPassword(resetData.CheckPassword)) return Ok(new { Status = false, Message = "密碼格式不符" });
            else if (!resetData.Password.Equals(resetData.CheckPassword)) return Ok(new { Status = false, Message = "填入密碼不同，請重新輸入" });

            // 生成密碼雜湊鹽
            string saltStr = HashPassword.CreateSalt();
            // 登入密碼加鹽雜湊結果
            string hashPassword = HashPassword.GenerateHashWithSalt(resetData.Password, saltStr);

            // 更新密碼，註銷驗證碼
            userQuery.CheckMailCode = "";
            userQuery.Salt = saltStr;
            userQuery.HashPassword = hashPassword;
            db.SaveChanges();

            // 重設成功
            return Ok(new { Status = true, Message = "密碼重設成功，請重新登入" });
        }

        /// <summary>
        /// 1-10 登出驗證票
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [SwaggerResponse(typeof(ApiJwtResult))]
        [Route("api/users/logout")]
        public IHttpActionResult Logout()
        {
            // 刷新 JwtToken 使其失效
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.RevokeToken();

            // 登出刷新
            return Ok(new { Status = true, JwtToken = jwtToken });
        }
    }
}
