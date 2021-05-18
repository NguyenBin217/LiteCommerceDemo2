using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class ChangePasswordController : Controller
    {
        // GET: ChangePassword
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string accountId = "", string oldPassword = "", string newPassword = "")
        {
            if (Request.HttpMethod == "POST")
            {
                bool account = AccountService.ChangePassword(accountId, CryptHelper.Md5(oldPassword), CryptHelper.Md5(newPassword));
                if (account == false)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng!");
                    return View();
                }
                // Quay về trang chủ
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}