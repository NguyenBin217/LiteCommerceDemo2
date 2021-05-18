using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            string connectionString = "server=.;user id=sa;password=123;database=LiteCommerceDB";
            var dal = new LiteCommerce.DataLayers.SQLServer.CountryDAL(connectionString);
            return Json(dal.List(), JsonRequestBehavior.AllowGet);
        }
    }
}