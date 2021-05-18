using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LiteCommerce.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // khoiwr tao cacs lopws tacs nghiep
            /*string cnStr = ConfigurationManager.ConnectionStrings["LiteCommerceDB"].ConnectionString;
            DataService.Init(DatabaseTypes.SQLServer, cnStr);*/

            // Khoi tao cac lop tac nghiep
            string cnStr = ConfigurationManager.ConnectionStrings["LiteCommerceDB"].ConnectionString;
            DataService.Init(BusinessLayers.DatabaseTypes.SQLServer, cnStr);
            ProductService.Init(DatabaseTypes.SQLServer, cnStr);
            AccountService.Init(DatabaseTypes.SQLServer, cnStr, AccountTypes.Employee);
        }
    }
}
