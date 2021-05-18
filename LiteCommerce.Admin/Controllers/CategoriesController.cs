using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int page = 1, string searchValue = "")
        {

            int rowCount = 0;
            int pageSize = 10;
            ViewBag.model = DataService.ListCategories(page,pageSize,searchValue,out rowCount);
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.RowCount = rowCount;
            int pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0) pageCount += 1;
            ViewBag.PageCount = pageCount;
            return View();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>


        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung thông tin loại hàng hóa";
            Categorie model = new Categorie()
            {
                CategoryID = 0
            };
            return View("Edit", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            if (Request.HttpMethod == "POST")
            {
                DataService.DeleteCategorie(id);
                return RedirectToAction("Index");
            }
            else
            {
                var model = DataService.GetCategory(id);
                if (model == null)
                    RedirectToAction("Index");
                return View(model);
            }
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Cập nhật thông tin loại hàng";
            Categorie model = DataService.GetCategorie(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Save(Categorie data)
        {
            try
            {
                //return Json(data);
                if (string.IsNullOrWhiteSpace(data.CategoryName))
                    ModelState.AddModelError("CategoryName", "Vui lòng nhập tên loại hàng hóa");
                if (string.IsNullOrWhiteSpace(data.Description))
                    data.Description = "";
                if (data.ParentCategoryId == 0)
                    data.ParentCategoryId = 0;
                if (!ModelState.IsValid)
                {
                    if (data.CategoryID == 0)
                        ViewBag.Title = "Bổ sung thông tin loại hàng hóa";
                    else ViewBag.Title = "Thay đổi thông tin loại hàng hóa";
                    return View("Edit", data);
                }
                if (data.CategoryID == 0)
                    DataService.AddCategorie(data);
                else
                    DataService.UpdateCategorie(data);
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Oops! trang này hình như không tồn tại :)");
            }
        }
    }
}