using NSwag.Annotations;
using PetEatsProject.Models;
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
    /// 訂單管理功能
    /// </summary>
    [OpenApiTag("Order", Description = "訂單管理功能")]
    public class OrderController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// 4-1 訂單-取得訂單詳細內容
        /// </summary>
        /// <param name="OrderInformationId">訂單資訊</param>
        /// <returns></returns>
        [HttpGet]
        [JwtAuthFilter]
        [Route("api/oder/detail")]
        public IHttpActionResult GetCartDetail(int OrderInformationId)
        {
            //查詢會員 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            // 查詢指定指定帳號
            var orderData = db.Orders.Where(x => x.OrderInformationId == OrderInformationId);

            //訂單資訊
            var OrderList = orderData.Select(x => new
            {

                x.Products.ProductName,
                x.ProductDetail,
                x.Products.Price,
                x.Amount,
                x.Memo,
            }).ToList();

            //訂單配送狀態
            var OrderStatus = orderData.Select(x => new
            {
                x.OrderInformation.OrderStatus.OrderStatusName,
                x.OrderInformation.Payment.PaymentName,
                x.OrderInformation.ProductDelivery.ProductDeliveryName,
                x.OrderInformation.CreatDate,
            }).FirstOrDefault();

            //訂單總金額
            var Ordersum = orderData.Sum(x => x.Products.Price * x.Amount + x.Products.Shop.Freight);


            return Ok(new { Status = true, Message = OrderList, OrderStatus, Ordersum });

        }

        /// <summary>
        /// 4-2 訂單-到付結帳
        /// </summary>
        /// <param name="Id">訂單資訊</param>
        /// <returns></returns>
        [HttpPost]
        [JwtAuthFilter]
        [Route("api/order/pay")]
        public IHttpActionResult OrderPay(int Id)
        {
            //查詢會員 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            //指定訂單
            var payList = db.OrderInformation.Find(Id);
            payList.OrderStatusId = (2);
            db.SaveChanges();

            return Ok(new { Status = true, Message = "訂單付款成功" });

        }

        /// <summary>
        /// 4-3 訂單-到付結帳後(取訂單配送狀態)
        /// </summary>
        /// <param name="Id">訂單資訊</param>
        /// <returns></returns>
        [HttpGet]
        [JwtAuthFilter]
        [Route("api/order/pay-result")]
        public IHttpActionResult GetPayDetail(int Id)
        {
            //查詢會員 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            //指定訂單
            var payContent = db.OrderInformation.Where(x => x.Id == Id).Select(x => new
            {
                x.OrderStatus.OrderStatusName
            });
            return Ok(new { Status = true, Message = "訂單配送狀態", data = payContent });

        }

        /// <summary>
        /// 4-4 訂單-送出訂單評價
        /// </summary>
        /// <param name="Id">指定訂單資料</param>
        /// <param name="Feedback"> 新增評價資料</param>
        /// <returns></returns>
        [HttpGet]
        [JwtAuthFilter]
        [Route("api/order/feedback")]
        public IHttpActionResult GetFeedback(int Id, string Feedback)
        {
            //查詢會員 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            //指定訂單
            var content = db.OrderInformation.Find(Id);
            content.Feedback = Feedback;
            db.SaveChanges();

            return Ok(new { Status = true, Message = "新增成功" });
        }

        /// <summary>
        /// 4-5 訂單-取得訂單列表簡介（包含狀態）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [JwtAuthFilter]
        [Route("api/order/Order-history")]
        public IHttpActionResult GetOrderStatus()
        {
            //查詢會員 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            //查詢指定指定帳號
            var oederData = db.Orders.Where(x => x.OrderInformation.UserId == userId);
            var ordertInfo = oederData.Select(x => new
            {
                x.OrderInformationId,
                x.Products.Shop.ShopName,
                x.OrderInformation.OrderStatus.OrderStatusName,
                x.Products.ProductName,
                x.ProductDetail

            }).Distinct();

            //var orderList = oederData.Select(x => new
            //{
            //    x.Products.ProductName,
            //    x.ProductDetail
            //}).ToList();

            ////============
            //var orderData = db.Orders.Where(x => x.OrderInformation.UserId == userId).ToArray();



            //foreach(var item in orderData)
            //{
            //    var list = new Order();
            //    item.OrderInformationId = list.OrderInformationId;
            //    //item.Products.Shop.ShopName
            //    //item.OrderInformation.OrderStatus.OrderStatusName.ToString();
            //    item.Products.ProductName = list.Products.ProductName;
            //    item.ProductDetail = list.ProductDetail;

            //}

            return Ok(new { Status = true, Message = ordertInfo/*, orderList,*/ });
        }

    }
}
