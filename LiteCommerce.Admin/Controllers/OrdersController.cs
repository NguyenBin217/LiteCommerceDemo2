using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiteCommerce.DomainModels;
using LiteCommerce.BusinessLayers;
using LiteCommerce.DataLayers.SQLServer;

namespace LiteCommerce.Admin.Controllers
{
    public class OrdersController : Controller
    {
        /// <summary>
        /// Trang quản lý đơn đặt hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="category"></param>
        /// <param name="supplier"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult List(int page = 1, int category = 0, int supplier = 0, string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 10;
            ViewBag.model = OrderService.Order_List(page, pageSize, category, supplier, searchValue, out rowCount);
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.RowCount = rowCount;
            int pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0) pageCount += 1;
            ViewBag.PageCount = pageCount;
            return View();
        }
        /// <summary>
        /// Tao trang san pham moi
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung thông tin hàng hóa ";
            Order model = new Order()
            {
                OrderID = 0
            };
            return View("Add", model);
        }

        /// <summary>
        /// Edit Product EX
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var model = OrderService.Get(id);

            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);

        }
        public ActionResult Delete(string method = "", int[] orderIDs = null)
        {
            try
            {
                if (orderIDs != null)
                {
                    OrderDAL.Delete_Order(orderIDs);

                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }
    }
}