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
    /// 購物車功能
    /// </summary>
    [OpenApiTag("Cart", Description = "購物車功能")]
    public class CartController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// 3-1 購物車-取得購物車內容
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [JwtAuthFilter]
        [Route("api/cart/content")]
        public IHttpActionResult GetCartDetail()
        {
            //查詢會員 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            //購物車是空的要給Message
            if (userId == null) { return Ok(new { Status = false, Message = "身分驗證失敗" }); }
            //指定訂單
            var cartData = db.Carts.Where(x => x.UserId == userId);
            var CartList = cartData.Select(x => new
            {
                x.Id,
                x.ProductDetailId,
                x.ProductDetail.ProductsId,
                x.ProductDetail.Products.ProductName,
                x.ProductDetail.Content,
                x.ProductDetail.Products.Price,
                x.Amount,
                x.Memo
            }).ToList();
            if (CartList.Count == 0) { return Ok(new { Status = false, Message = "購物車是空的" }); }

            var CartSum = cartData.Sum(x => x.ProductDetail.Products.Price * x.Amount + x.ProductDetail.Products.Shop.Freight);

            return Ok(new { Status = true, Message = CartList, total = CartSum });


        }

        /// <summary>
        /// 3-2 購物車-商品加入購物車
        /// </summary>
        /// <param name="ProductDetailId">新增訂單商品詳細資料</param>
        /// <param name="Memo">新增備註資料</param>
        /// <param name="Amount">新增數量資料</param>
        /// <returns></returns>
        [HttpPost]
        [JwtAuthFilter]
        [Route("api/cart/AddProduct")]
        public IHttpActionResult AddProductToCart(int ProductDetailId, string Memo, int Amount)
        {
            //查詢會員 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            var cartData = db.Carts.Where(x => x.UserId == userId);
            //找相同的品項
            var productList = cartData.Select(x => x.ProductDetailId).ToList();

            var cart = new Cart();
            cart.UserId = userId;
            cart.ProductDetailId = ProductDetailId;
            cart.Memo = Memo;
            cart.Amount = Amount;
            cart.CreatDate = DateTime.Now;

            if (cartData.FirstOrDefault() == null)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return Ok(new { Status = true, Message = "新增成功" });
            }
            if (productList.Contains(ProductDetailId))
            {
                var productAmount = cartData.FirstOrDefault(x => x.UserId == userId);
                productAmount.Memo = Memo;
                productAmount.Amount = Amount;
                productAmount.CreatDate = DateTime.Now;
                db.SaveChanges();
                return Ok(new { Status = true, Message = "相同商品更新成功" });
            }
            //找相同店家
            var shopId = cartData.Select(x => new { x.ProductDetail.Products.ShopId }).FirstOrDefault();
            //找相同店家的商品
            var sameShop = db.ProductDetails.Where(x => x.Products.ShopId == shopId.ShopId).Select(x => x.Id).ToList();

            if (sameShop.Contains(ProductDetailId))
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return Ok(new { Status = true, Message = "新增成功" });
            }
            return Ok(new { Status = false, Message = "您的購物車已有其他商家的訂單，請選擇同商家的商品" });

        }

        /// <summary>
        /// 3-3 購物車-編輯品項數量
        /// </summary>
        /// <param name="Id">指定訂單資料</param>
        /// <param name="ProductDetailId">編輯訂單詳細資料</param>
        /// <param name="Amount">編輯數量</param>
        /// <param name="Memo">編輯備註</param>
        /// <returns></returns>
        [JwtAuthFilter]
        [HttpPut]
        [Route("api/cart/item")]
        public IHttpActionResult EditProductAmountToCart(int Id, int ProductDetailId, int Amount, string Memo)
        {
            //查詢會員 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            //指定訂單
            var cartList = db.Carts.Find(Id);
            // 設定更新值
            if (cartList.Amount == 0)
            {
                db.Carts.Remove(cartList);
                db.SaveChanges();
                return Ok(new { Status = true, Message = "已刪除訂單" });
            }
            else
            {
                cartList.ProductDetailId = ProductDetailId;
                cartList.Memo = Memo;
                cartList.Amount = Amount;
            }
            db.SaveChanges();
            return Ok(new { Status = true, Message = "編輯訂單成功" });

        }

        /// <summary>
        /// 3-4 購物車-刪除購物車
        /// </summary>
        /// <param name="Id">刪除購物車的訂單資料</param>
        /// <returns></returns>
        [HttpDelete]
        [JwtAuthFilter]
        [Route("api/cart/delete")]
        public IHttpActionResult DeleteCart(int Id)
        {
            //查詢會員 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            //指定訂單
            Cart cart = db.Carts.Find(Id);
            db.Carts.Remove(cart);
            db.SaveChanges();
            return Ok(new { Status = true, Message = "已刪除訂單" });
        }

        /// <summary>
        /// 3-5 購物車-送出購物車資料
        /// </summary>
        /// <param name="PaymentId">選擇付款方式</param>
        /// <param name="ProductDeliveryId">選擇送貨方式</param>
        /// <returns></returns>
        [HttpGet]
        [JwtAuthFilter]
        [Route("api/cart/send")]
        public IHttpActionResult SendDetail(int PaymentId, int ProductDeliveryId)
        {
            //查詢會員 解密 JwtToken 取出資料回傳
            var userToken = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            // 單純刷新效期不新生成，新生成會進資料庫
            JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
            string jwtToken = jwtAuthUtil.ExpRefreshToken(userToken);
            int userId = (int)userToken["Id"];

            //送出要結帳的訂單資訊
            var orderList = db.OrderInformation.Where(x => x.UserId == userId);
            var oList = new OrderInformation();
            oList.UserId = userId;
            oList.OrderStatusId = (1);
            oList.PaymentId = PaymentId;
            oList.ProductDeliveryId = ProductDeliveryId;
            oList.CreatDate = DateTime.Now;
           
            //如果購物車是空的送出失敗
            var userList = db.Carts.Where(x => x.UserId == userId).ToList();
            if (userList.Count == 0) { return Ok(new { Status = false, Message = "訂單送出失敗" }); }
            //購物車裡有商品
            if (orderList != null)
            {
                db.OrderInformation.Add(oList);
                db.SaveChanges();



                //指定訂單
                var catrList = db.Carts.Where(x => x.UserId == userId).ToArray();
                foreach (var i in catrList)
                {
                    var order = new Order();
                    //送出訂單
                    order.ProductsId = i.ProductDetail.ProductsId;
                    order.OrderInformationId = oList.Id;
                    order.ProductName = i.ProductDetail.Products.ProductName;
                    order.ProductDetail = i.ProductDetail.Content;
                    order.Price = i.ProductDetail.Products.Price;
                    order.Amount = i.Amount;
                    db.Orders.Add(order);
                }
                db.SaveChanges();

                //移除購物車資訊
                var carts = userList.ToArray();
                foreach (var i in carts)
                {
                    db.Carts.Remove(i);
                    db.SaveChanges();
                }
            }
            return Ok(new { Status = true, Message = "訂單送出成功" });

            //return Ok(new { Status = true, Message = "訂單送出成功" });
        }

    }
}
