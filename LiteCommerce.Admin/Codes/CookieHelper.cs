using LiteCommerce.DomainModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin
{
    public static class CookieHelper
    {
        /// <summary>
        /// Chuyển đối tượng kiểu Account về thành 1 JSON string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AccountToCookieString(Account value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Chuyển chuỗi JSON về thành đối tượng kiểu Account
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Account CookieStringToAccount(string value)
        {
            return JsonConvert.DeserializeObject<Account>(value);
        }

        public static ChangePassword CookieStringToChangePassword(string value)
        {
            return JsonConvert.DeserializeObject<ChangePassword>(value);
        }
    }
}