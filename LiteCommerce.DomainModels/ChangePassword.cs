using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class ChangePassword
    {
        public string AccountID { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

    }
}
