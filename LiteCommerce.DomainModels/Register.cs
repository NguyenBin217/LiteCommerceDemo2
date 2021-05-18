using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class Register
    {
       /* public long GalleryID { get; set; }

        public int ProductID { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsHidden { get; set; }*/

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
