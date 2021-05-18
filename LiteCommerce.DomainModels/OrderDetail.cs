using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class OrderDetail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public double SalePrice { get; set; }
        public int Quantity { get; set; }
        public double Total()
        {
            return SalePrice * Quantity;
        }
    }
}