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
    public class ProductsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// List search Product 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="category"></param>
        /// <param name="supplier"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult List(int page = 1, int category= 0, int supplier= 0, string searchValue = "")
        {
            int rowCount = 0;
            int pageSize = 10;
            ViewBag.model = ProductService.List(page, pageSize, category, supplier, searchValue, out rowCount);
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.RowCount = rowCount;
            int pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0) pageCount += 1;
            ViewBag.PageCount = pageCount;
            return View();
        }
        /// <summary>
        /// Edit Product EX
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit( int id)
        {
            var model = ProductService.GetEx(id);

            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);

        }
        /// <summary>
        /// Product Add 
        /// </summary>
        /// <returns></returns>

        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung thông tin hàng hóa ";
            Product model = new Product()
            {
                ProductID = 0
            };
            return View("Add", model);
        }
        /// <summary>
        /// Delete Attribute ProductEX
        /// </summary>
        /// <param name="id"></param>
        /// <param name="attributesIds"></param>
        /// <returns></returns>


        public ActionResult DeleteGallery(int id,long[] galleryIds)
        {
            int id1 = id;
            if (galleryIds == null || galleryIds.Length == 0)
                return RedirectToAction("Edit", new { id = id1});
            ProductService.DeleteGalleries(galleryIds);
            return RedirectToAction("Edit", new { id = id1});

        }

        public ActionResult DeleteAttributes(int id, long[] attributesIds)
        {
            int id1 = id;
            if (attributesIds == null || attributesIds.Length == 0)
                return RedirectToAction("Edit", new { id = id1 });
            ProductService.DeleteAttributes(attributesIds);
            return RedirectToAction("Edit", new { id = id1 });

        }
        /// <summary>
        /// Add Attributes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddAttribute(int id)
        {
            int id1 = id;
            ViewBag.Title = "Bổ sung thuộc tính sản phẩm";
            ProductAttribute model = new ProductAttribute()
            {
                ProductID = id1
            };
            return View("EditAttribute", model);
        }
        /// <summary>
        /// Edit Attribute
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditAttribute(int id)
        {
            ViewBag.Title = "Cập nhật thông tin thuộc tính";
            ProductAttribute model = ProductService.GetAttribute(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }



        public ActionResult AddGallery(int id)
        {
            int id1 = id;
            ViewBag.Title = "Bổ sung thuộc tính sản phẩm";
            ProductGallery model = new ProductGallery()
            {
                ProductID = id1
            };
            return View("EditGallery", model);
        }
        public ActionResult EditGallery(int id)
        {
            ViewBag.Title = "Cập nhật thông tin thuộc tính";
            ProductGallery model = ProductService.GetGallery(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        /// <summary>
        /// Save edit and add Product
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ActionResult Save(Product data)
        {

            if (string.IsNullOrWhiteSpace(data.ProductName))
                ModelState.AddModelError("ProductName", "Tên hàng rỗng");
            if (string.IsNullOrWhiteSpace(data.Unit))
                ModelState.AddModelError("Unit", "Đơn vị tính rỗng");

            /*return Json(data, JsonRequestBehavior.AllowGet);*/
            if (!ModelState.IsValid)
            {
                if (data.ProductID == 0)
                {
                    ViewBag.Title = "Bổ sung nhà cung cấp";
                }
                else
                {
                    ViewBag.Title = "Cập nhật thông tin của nhà cung cấp";
                }
                return View("Edit", data);
            }

            if (data.ProductID == 0)
            {
                ProductService.Add(data);
            }
            else
            {
                ProductService.Update(data);
            }
            return RedirectToAction("Edit", new { id = data.ProductID });
            /*return RedirectToAction("Index", "Products");*/
        }

        public ActionResult SaveAttributes(ProductAttribute data)
        {
            /*return Json(data, JsonRequestBehavior.AllowGet);*/
            if (data.AttributeID == 0)
            {
                ProductService.AddAttribute(data);
            }
            else
            {
                ProductService.UpdateAttribute(data);
            }
            return RedirectToAction("Edit", new { id = data.ProductID });
            /*return RedirectToAction("Index", "Products");*/
        }



        public ActionResult SaveGalleries(ProductGallery data)
        {
            /*return Json(data, JsonRequestBehavior.AllowGet);*/
            if (data.GalleryID == 0)
            {
                ProductService.AddGallery(data);
            }
            else
            {
                ProductService.UpdateGallery(data);
            }
            return RedirectToAction("Edit", new { id = data.ProductID });
            /*return RedirectToAction("Index", "Products");*/
        }

        /// <summary>
        /// Delete Product 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var model = DataService.GetProduct(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "GET")
            {
                return View(model);
            }
            DataService.DeleteProduct(id);
            return RedirectToAction("Index");

        }


    }
}