using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;

namespace PetEatsProject.Security
{
    /// <summary>
    /// 信件
    /// </summary>
    public class Mail
    {
        /// <summary>
        /// 生成信箱驗證連結
        /// </summary>
        /// <param name="urlHost"></param>
        /// <param name="mailGuid"></param>
        /// <returns></returns>
        public static string SetAuthMailLink(string urlHost, string mailGuid)
        {
            //string verifyLink = @"https://" + urlHost + @"/#api/Users_AuthMail?guid=" + mailGuid;
            string ghPageURL = "learn-at-rocketcamp.github.io/project-peteats-2022";
            string verifyLink = @"https://" + ghPageURL + @"/#/auth-mail?guid=" + mailGuid;

            return verifyLink;
        }

        /// <summary>
        /// 生成忘記密碼重設連結
        /// </summary>
        /// <param name="urlHost"></param>
        /// <param name="mailGuid"></param>
        /// <returns></returns>
        public static string SetResetPasswordMailLink(string urlHost, string mailGuid)
        {
            string ghPageURL = "learn-at-rocketcamp.github.io/project-peteats-2022";
            string verifyLink = @"https://" + ghPageURL + @"/#/reset-password?guid=" + mailGuid;

            return verifyLink;
        }


        /// <summary>
        /// 郵件格式正規檢查
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool RegexEmail(string str)
        {
            // 郵件格式 ccc@kmit.edu.tw
            return Regex.IsMatch(str, @"[a-zA-Z0-9_]+@[a-zA-Z0-9\._]+");
        }

        /// <summary>
        /// 密碼格式正規檢查
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool RegexPassword(string str)
        {
            // 密碼長度必須有八碼，包含英數字
            return Regex.IsMatch(str, @"^.*(?=.{8,})(?=.*\d)(?=.*[a-zA-Z]).*$");
        }

        /// <summary>
        /// 台灣手機格式正規檢查
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool RegexMobilePhoneTW(string str)
        {
            // 台灣手機號碼格式
            return Regex.IsMatch(str, @"^09[0-9]{8}$");
        }

        /// <summary>
        /// 日期格式正規檢查 YYYY/MM/DD
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool RegexDate(string str)
        {
            // 日期格式
            return Regex.IsMatch(str, @"#^((19|20)?[0-9]{2}[- /.](0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01]))*$#");
        }


        /// <summary>
        /// 發送驗證信
        /// </summary>
        /// <param name="toName">收信人名稱</param>
        /// <param name="toAddress">收信地址</param>
        /// <param name="verifyLink">驗證連結</param>
        public static void SendVerifyLinkMail(string toName, string toAddress, string verifyLink)
        {
            string fromAddress = WebConfigurationManager.AppSettings["gmailAccount"];
            string fromName = "Pet Eats有限公司";
            string title = "會員認證通知";
            string mailAccount = WebConfigurationManager.AppSettings["gmailAccount"];
            string mailPassword = WebConfigurationManager.AppSettings["gmailPassword"];

            //建立建立郵件
            MimeMessage mail = new MimeMessage();
            // 添加寄件者
            mail.From.Add(new MailboxAddress(fromName, fromAddress));
            // 添加收件者
            mail.To.Add(new MailboxAddress(toName, toAddress.Trim()));
            // 設定郵件標題
            mail.Subject = title;
            //使用 BodyBuilder 建立郵件內容
            BodyBuilder bodyBuilder = new BodyBuilder
            {
                HtmlBody =
                "<h1>Pet Eats 帳號開通連結</h1>" +
                $"<h3>請點選以下連結進行帳號開通登入，如未開通帳號將無法使用網站進階功能!</h3>" +
                $"<a href='{verifyLink}'>{verifyLink}</a>"
            };
            //設定郵件內容
            mail.Body = bodyBuilder.ToMessageBody(); //轉成郵件內容格式

            using (var client = new SmtpClient())
            {
                //有開防毒時需設定關閉檢查
                client.CheckCertificateRevocation = false;
                //設定連線 gmail ("smtp Server", Port, SSL加密) 
                client.Connect("smtp.gmail.com", 587, false); // localhost 測試使用加密需先關閉 

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(mailAccount, mailPassword);

                client.Send(mail);
                client.Disconnect(true);
            }
        }

        /// <summary>
        /// 發送驗證信
        /// </summary>
        /// <param name="toName">收信人名稱</param>
        /// <param name="toAddress">收信地址</param>
        /// <param name="resetLink">重設連結</param>
        public static void SendResetLinkMail(string toName, string toAddress, string resetLink)
        {
            string fromAddress = WebConfigurationManager.AppSettings["gmailAccount"];
            string fromName = "Pet Eats有限公司";
            string title = "密碼重設通知";
            string mailAccount = WebConfigurationManager.AppSettings["gmailAccount"];
            string mailPassword = WebConfigurationManager.AppSettings["gmailPassword"];

            //建立建立郵件
            MimeMessage mail = new MimeMessage();
            // 添加寄件者
            mail.From.Add(new MailboxAddress(fromName, fromAddress));
            // 添加收件者
            mail.To.Add(new MailboxAddress(toName, toAddress));
            // 設定郵件標題
            mail.Subject = title;
            //使用 BodyBuilder 建立郵件內容
            BodyBuilder bodyBuilder = new BodyBuilder
            {
                HtmlBody =
                "<h1>Pet Eats 密碼重設連結</h1>" +
                $"<h3>請點選以下連結進行密碼重設!</h3>" +
                $"<a href='{resetLink}'>{resetLink}</a>"
            };
            //設定郵件內容
            mail.Body = bodyBuilder.ToMessageBody(); //轉成郵件內容格式

            using (var client = new SmtpClient())
            {
                //有開防毒時需設定關閉檢查
                client.CheckCertificateRevocation = false;
                //設定連線 gmail ("smtp Server", Port, SSL加密) 
                client.Connect("smtp.gmail.com", 587, false); // localhost 測試使用加密需先關閉 

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(mailAccount, mailPassword);

                client.Send(mail);
                client.Disconnect(true);
            }
        }

        /// <summary>
        /// 發送聯絡我們訊息
        /// </summary>
        /// <param name="toName">收信者名稱</param>
        /// <param name="toAddress">收信者地址</param>
        /// <param name="userName">會員暱稱</param>
        /// <param name="userAccount">會員帳號=>收信地址</param>
        /// <param name="userMessage">會員留言訊息</param>
        public static void SendMessageToUs(string toName, string toAddress, string userName, string userAccount, string userMessage)
        {
            string fromAddress = WebConfigurationManager.AppSettings["gmailAccount"];
            string fromName = "Pet Eats有限公司";
            string title = "會員站內反應信";
            string mailAccount = WebConfigurationManager.AppSettings["gmailAccount"];
            string mailPassword = WebConfigurationManager.AppSettings["gmailPassword"];

            //建立建立郵件
            MimeMessage mail = new MimeMessage();
            // 添加寄件者
            mail.From.Add(new MailboxAddress(fromName, fromAddress));
            // 添加收件者
            mail.To.Add(new MailboxAddress(toName, toAddress));
            // 設定郵件標題
            mail.Subject = title;
            //使用 BodyBuilder 建立郵件內容
            BodyBuilder bodyBuilder = new BodyBuilder
            {
                HtmlBody =
                "<h1>Pet Eats-會員站內訊息</h1>" +
                $"<h3>會員帳號 : {userAccount}</h3>" +
                $"<h3>留言內容 : </h3>" +
                $"<p>{userMessage}</p>"
            };
            //設定郵件內容
            mail.Body = bodyBuilder.ToMessageBody(); //轉成郵件內容格式

            using (var client = new SmtpClient())
            {
                //有開防毒時需設定關閉檢查
                client.CheckCertificateRevocation = false;
                //設定連線 gmail ("smtp Server", Port, SSL加密) 
                client.Connect("smtp.gmail.com", 587, false); // localhost 測試使用加密需先關閉 

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(mailAccount, mailPassword);

                client.Send(mail);
                client.Disconnect(true);
            }
        }
    }
}