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
    public class SuppliersController : Controller
    {
        // GET: Suppliers
        /// <summary>
        /// Tìm kiếm, hiển thị danh sách các nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
      
            return View();

        }
        /// <summary>
        /// List Supplier Search
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult List(int page = 1, string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 10;
            ViewBag.model = DataService.ListSuppliers(page, pageSize, searchValue, out rowCount);
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.RowCount = rowCount;
            int pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0) pageCount += 1;
            ViewBag.PageCount = pageCount;
            return View();
        }
        /// <summary>
        /// Add nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung nhà cung cấp";
            Supplier model = new Supplier()
            {
                SupplierID = 0
            };
            return View("Edit",model);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Cập nhật thông tin của nhà cung cấp";

            Supplier model = DataService.GetSupplier(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            if (Request.HttpMethod == "POST")
            {

                //Xóa Supplier có mã là id
                DataService.DeleteSupplier(id);
                //Quay về trang Index
                return RedirectToAction("Index");
            }
            else
            {
                //Lấy thông tin của Supplier cần xóa
                var model = DataService.GetSupplier(id);
                if (model == null)
                    RedirectToAction("Index");
                return View(model);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Supplier data)
        {
            /*return Json(data, JsonRequestBehavior.AllowGet);*/

            if (string.IsNullOrWhiteSpace(data.SupplierName))
                ModelState.AddModelError("SupplierName", "Vui lòng nhập tên nhà cung cấp");
            if (string.IsNullOrWhiteSpace(data.ContactName))
                ModelState.AddModelError("ContactName", "Bạn chưa nhập tên liên hệ của nhà cung cấp");
            if (string.IsNullOrWhiteSpace(data.Address))
                data.Address = "";
            if (string.IsNullOrWhiteSpace(data.Country))
                data.City = "";
            if (string.IsNullOrWhiteSpace(data.PostalCode))
                data.PostalCode = "";
            if (string.IsNullOrWhiteSpace(data.Phone))
                data.PostalCode = "";



            if (!ModelState.IsValid)
            {
                if(data.SupplierID == 0)
                {
                    ViewBag.Title = "Bổ sung nhà cung cấp";
                } else
                {
                    ViewBag.Title = "Cập nhật thông tin của nhà cung cấp";
                }
            }
            if (!ModelState.IsValid)
            {
                return View("Edit", data);
            }

            if (data.SupplierID == 0)
            {
                DataService.AddSupplier(data);
            } else
            {
                DataService.UpdateSupplier(data);
            }
            return RedirectToAction("Index", "Suppliers");
        }
    }
}