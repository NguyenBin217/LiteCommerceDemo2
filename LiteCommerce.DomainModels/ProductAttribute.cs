using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class ProductAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public long AttributeID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ProductID { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public int DisplayOrder { get; set; }
        
    }
}
