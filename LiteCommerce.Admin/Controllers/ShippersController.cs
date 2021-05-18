using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class ShippersController : Controller
    {
        // GET: Shippers
        public ActionResult Index()
        {
           
            return View();
        }
        /// <summary>
        /// List ds tim kiem Shipper
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>

        public ActionResult List(int page = 1, string searchValue = "")
        {

            int rowCount = 0;
            int pageSize = 10;
            ViewBag.model = DataService.ListShippers(page, pageSize, searchValue, out rowCount);
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.RowCount = rowCount;
            int pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0) pageCount += 1;
            ViewBag.PageCount = pageCount;
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Thay đổi thông tin nhà vận chuyển";
            var model = DataService.GetShipper(id);
            if (model == null)
                RedirectToAction("Index");
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var model = DataService.GetShipper(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "GET")
            {
                return View(model);
            }
            DataService.DeleteShipper(id);
            return RedirectToAction("Index");

        }
        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung thông tin nhà vận chuyển";
            Shipper model = new Shipper()
            {
                ShipperID = 0
            };
            return View("Edit", model);
        }



        public ActionResult Save(Shipper data)
        {
            /*return Json(data, JsonRequestBehavior.AllowGet);*/

            if (data.ShipperID == 0)
            {
                DataService.AddShipper(data);
            }
            else
            {
                DataService.UpdateShipper(data);
            }
            return RedirectToAction("Index", "Shippers");
        }

    }
}