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
    public class EmployeesController : Controller
    {
        // GET: Employees
        /// <summary>
        /// Tìm kiếm, hiển thị danh sách các nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            
            return View();
        }
        /// <summary>
        /// List nhân viên
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>

        public ActionResult List(int page = 1, string searchValue = "")
        {

            int rowCount = 0;
            int pageSize = 10;
            ViewBag.model = DataService.ListEmployees(page, pageSize, searchValue, out rowCount);
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.RowCount = rowCount;
            int pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0) pageCount += 1;
            ViewBag.PageCount = pageCount;
            return View();
        }
        /// <summary>
        /// bo sung nha cung cap
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewBag.Title = "Bổ sung nhân viên";
            Employee model = new Employee()
            {
                EmployeeID = 0
            };
            return View("Edit",model);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Cập nhật thông tin của nhân viên";
            Employee model = DataService.GetEmployee(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var model = DataService.GetEmployee(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "GET")
            {
                return View(model);
            }
            DataService.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Save(Employee data)
        {
            if (data.EmployeeID == 0)
            {
                DataService.AddEmployee(data);
            }
            else
            {
                DataService.UpdateEmployee(data);
            }
            return RedirectToAction("Index", "Employees");
        }
    }
}