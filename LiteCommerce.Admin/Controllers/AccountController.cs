using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LiteCommerce.Admin.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [AllowAnonymous]
        public ActionResult Login(string userName = "", string password = "")
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = Convert.ToString(Session["Supplier_SearchValue"]);
            }

            Session["userName"] = userName;
            if (Request.HttpMethod == "POST")
            {
                var account = AccountService.Authorize(userName, CryptHelper.Md5(password));
                if (account == null)
                {
                    ModelState.AddModelError("", "Thông tin đăng nhập bị sai");
                    return View();
                }

                // Ghi nhận Cookie cho phiên đăng nhập
                FormsAuthentication.SetAuthCookie(CookieHelper.AccountToCookieString(account), false);
                // Quay về trang chủ
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");

        }
    }
}