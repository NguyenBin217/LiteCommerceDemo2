using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class ProductGallery
    {
        /// <summary>
        /// 
        /// </summary>
        public long GalleryID { get; set; }
        public int ProductID { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public int IsHidden { get; set; }



    }
}
