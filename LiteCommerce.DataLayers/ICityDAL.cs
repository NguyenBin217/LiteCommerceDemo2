using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai bao cac chuc nang xu ly du lieu lien quan den city
    /// </summary>
    public interface ICityDAL
    {
        /// <summary>
        /// Danh sách tất cả các thành phố
        /// </summary>
        List<City> List();

        /// <summary>
        /// Danh sách các thành phố thuộc 1 quốc gia
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        List<City> List(string countryName);
    }
}
