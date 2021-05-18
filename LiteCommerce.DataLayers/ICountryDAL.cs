using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai baso cac chuc nang xu ly lien quan den country
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Country> List();
        
    }
}
