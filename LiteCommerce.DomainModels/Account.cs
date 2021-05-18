using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Tài khoản đăng nhập hệ thống
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Mã tài khoản (mã Employee, mã Customer)
        /// </summary>
        public string AccountID { get; set; }

        /// <summary>
        /// Tên gọi đầy đủ
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        public string Photo { get; set; }
    }
}
