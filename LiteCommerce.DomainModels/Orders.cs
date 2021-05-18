using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// 
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int EmployeeID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AcceptTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ShipperID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ShippedTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime FinishedTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Satus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ShipperName { get; set; }
    }
}
