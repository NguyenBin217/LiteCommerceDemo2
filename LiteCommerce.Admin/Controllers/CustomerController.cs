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
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult List(int page = 1, string searchValue = "")
        {

            int rowCount = 0;
            int pageSize = 10;
            ViewBag.model = DataService.ListCustomers(page, pageSize, searchValue, out rowCount);
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.RowCount = rowCount;
            int pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0) pageCount += 1;
            ViewBag.PageCount = pageCount;
            return View();
        }
        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung khách hàng";
            Customer model = new Customer()
            {
                CustomerID = 0
            };
            return View("Edit", model);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Cập nhật thông tin của khách hàng";
            Customer model = DataService.GetCustomer(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var model = DataService.GetCustomer(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "GET")
            {
                return View(model);
            }
            DataService.DeleteCustomer(id);
            return RedirectToAction("Index");
        }
        public ActionResult Save(Customer data)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", data);
            }

            if (data.CustomerID == 0)
            {
                DataService.AddCustomer(data);
            }
            else
            {
                DataService.UpdateCustomer(data);
            }
            return RedirectToAction("Index", "Customer");
        }
    }
}