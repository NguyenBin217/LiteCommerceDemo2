using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LiteCommerce.Admin.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index(string fullName = "", string email = "", string password = "", string retypePassword = "", string photo = "")
        {
            if (Request.HttpMethod == "POST")
            {

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    fullName = Convert.ToString(Session["fullName"]);
                }
                if (string.IsNullOrWhiteSpace(email))
                {
                    email = Convert.ToString(Session["email"]);
                }
                Session["fullName"] = fullName;
                Session["email"] = email;
                var emailExist = AccountService.EmailExist(email);
                if (emailExist != "")
                {
                    ModelState.AddModelError("email", "Email đã tồn tại");
                    return View();
                }
                if (password != retypePassword)
                {
                    ModelState.AddModelError("retypePassword", "Nhập lại mật khẩu không đúng");
                    return View();
                }
                var names = fullName.Split(' ');
                var firstName = "";
                var lastName = "";
                if (names.Length == 1)
                {
                   firstName = names[0];
                } else
                {
                    firstName = names[1];
                   lastName = names[0];
                }
                AccountService.Register(lastName, firstName, email, CryptHelper.Md5(password), photo);
                /* return Json(account, JsonRequestBehavior.AllowGet);*/
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }
    }
}