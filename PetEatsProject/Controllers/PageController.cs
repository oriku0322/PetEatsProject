using NSwag.Annotations;
using PetEatsProject.Models;
using PetEatsProject.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;

namespace PetEatsProject.Controllers
{
    /// <summary>
    /// 網頁操作功能
    /// </summary>
    [OpenApiTag("GET", Description = "網頁操作功能")]
    public class PageController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// 測試
        /// </summary>
        /// <param name="userData">註冊資料</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/page/hello")]
        public IHttpActionResult Text()
        {

            return Ok(new { Status = "success", Message = "HELLO" });

        }

        ///// <summary>
        ///// 測試
        ///// </summary>
        ///// <param name="id">註冊資料</param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("api/shop/text")]
        //public IHttpActionResult Text(int id)
        //{
        //    // 查詢指定帳號
        //    var shopDate = db.Shops.FirstOrDefault(x => x.Id == id);

        //    return Ok(new { Status = "success", Message = shopDate });

        //}

        /// <summary>
        /// 2-1 首頁-取得分類
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/home/shop")]
        public IHttpActionResult GetShopClass()
        {
            //查詢指定分類名稱ID
            var className = db.ProductClasses.Select(x => new
            {
                x.Id,
                x.ProductClassName,
                imageUrl = "https://" + Request.RequestUri.Host + "/Image/Class/" + x.Image
            }).ToList();

            return Ok(new { Status = true, Data = className });
        }

        /// <summary>
        /// 2-2 首頁-取得縣市
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/home/city")]
        public IHttpActionResult GetShopCity()
        {
            //查詢縣市名稱ID
            var cityId = db.Cities.Select(x => new
            {
                x.Id,
                x.CityName,
                imageUrl = "https://" + Request.RequestUri.Host + "/Image/City/" + x.Image
            }).ToList();


            return Ok(new { Status = true, Message = cityId });
        }


        /// <summary>
        /// 2-3 首頁-取得熱門店家x4
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/home/hot")]
        public IHttpActionResult GetHotShop()
        {
            // 查詢最高瀏覽前4名
            var shopName = db.Shops.OrderByDescending(x => x.Views).Select(x => new
            {
                Id = x.Id,
                CityName = x.City.CityName,
                ShopName = x.ShopName,
                Views = x.Views,
                EvaluateStars = x.EvaluateStars,
                Freight = x.Freight,
                imageUrl = "https://" + Request.RequestUri.Host + "/Image/Shop/" + x.Image
            }).ToList();
            var shopViews = shopName.Take(4);

            return Ok(new { Status = true, Message = shopViews });

        }

        /// <summary>
        /// 2-4 內頁-依品項分類取得商家簡介列表
        /// </summary>
        /// <param name="ProductClassId">依品項分類取得商家簡介列表</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/shop/tag")]
        public IHttpActionResult GetClassShop(int ProductClassId)
        {
            //string oururl = WebConfigurationManager.AppSettings["url"];

            //查詢指定分類的商家
            var classList = db.Products.Where(x => x.ProductClassId == ProductClassId).Select(x => new
            {
                x.Shop.Id,
                x.Shop.City.CityName,
                x.Shop.ShopName,
                x.Shop.OpeningHourse,
                x.Shop.ShopTEL,
                x.Shop.ShopAddress,
                x.Shop.Freight,
                x.Shop.Views,
                x.Shop.EvaluateStars,
                imageUrl = "https://" + Request.RequestUri.Host + "/Image/Shop/" + x.Shop.Image
            }).Distinct();



            return Ok(new { Status = true, data = classList });
        }

        /// <summary>
        /// 2-5 內頁-依縣市ID取得商家列表
        /// </summary>
        /// <param name="Id">依縣市ID取商家資料</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/shop/local/city")]
        public IHttpActionResult GetCityShop(int Id)
        {
            //查詢指定縣市的商家
            var cityList = db.Shops.Where(x => x.CityId == Id).Select(x => new
            {
                x.Id,
                x.City.CityName,
                x.ShopName,
                x.OpeningHourse,
                x.ShopTEL,
                x.ShopAddress,
                x.Freight,
                x.Views,
                x.EvaluateStars,
                imageUrl = "https://" + Request.RequestUri.Host + "/Image/City/" + x.Image
            });

            return Ok(new { Status = true, Data = cityList });

        }

        /// <summary>
        /// 2-6 內頁-取得商家的詳細資料、菜單資料、評價
        /// </summary>
        /// <param name = "ShopId">商家的詳細資料</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/shop/shop-id/menu")]
        public IHttpActionResult GetShopMenu(int ShopId)
        {
            //顯示店家的詳細資料
            var shopData = db.Products.Where(x => x.ShopId == ShopId);
            var shopList = shopData.Select(x => new
            {
                x.Shop.ShopName,
                x.Shop.OpeningHourse,
                x.Shop.ShopTEL,
                x.Shop.ShopAddress,
                x.Shop.Freight,
                x.Shop.Views,
                x.Shop.EvaluateStars,
                imageUrl = "https://" + Request.RequestUri.Host + "/Image/Shop/" + x.Shop.Image
            }).FirstOrDefault();

            //顯示菜單的詳細資料
            var menuList = shopData.Select(x => new
            {
                x.Id,
                x.ProductName,
                x.Price,
                imageUrl = "https://" + Request.RequestUri.Host + "/Image/" + x.Image
            }).ToList();

            //顯示店家評價
            var feedback = db.Orders.Where(x => x.Products.ShopId == ShopId).Select(x => new
            {
                x.OrderInformation.User.Nickname,
                x.OrderInformation.Feedback

            }).AsEnumerable();

            return Ok(new { Status = true, Message = shopList, menuList, feedback });

        }


        /// <summary>
        /// 2-7 內頁-取得（該店該餐點）的詳細資料
        /// </summary>
        /// <param name = "ProductId">餐點的詳細資料</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/shop/shop-id/menu/item-id")]
        public IHttpActionResult GetProductDetails(int ProductId)
        {
            //顯示餐點的詳細資料
            var productData = db.ProductDetails.Where(x => x.ProductsId == ProductId);
            var productsList = productData.Select(x => new
            {
                x.Products.ProductName,
                x.Products.Price,
                imageUrl = "https://" + Request.RequestUri.Host + "/Image/" + x.Products.Image
            }).FirstOrDefault();

            var DetailList = productData.Select(x => new
            {
                x.Id,
                x.Content
            }).ToList();
            return Ok(new { Status = true, Data = productsList, DetailList });

        }

        /// <summary>
        /// 2-8 增加店家瀏覽次數
        /// </summary>
        /// <param name="ShopId">店家瀏覽資料</param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerResponse(typeof(ApiMessageResult))]
        [Route("api/shop/views")]
        public IHttpActionResult AddActivityViews(int ShopId)
        {
            try
            {
                // 查詢指定活動
                var shopQuery = db.Shops.FirstOrDefault(x => x.Id == ShopId);

                // 增加瀏覽次數
                shopQuery.Views += 1;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                return Ok(new { Status = false, Message = ex.Message });
            }

            return Ok(new { Status = true, Message = "店家瀏覽次數增加成功" });
        }

    }
}
