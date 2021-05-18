using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai bao cac chuc nang xu ly du lieu lien quan den nha vận chuyển
    /// </summary>
    public interface IShipperDAL
    {
        /// <summary>
        /// Lay danh sach nha cung cap
        /// </summary>
        /// <param name="page">trang can lay du lieu</param>
        /// <param name="pagesize">so dong tren moi trang</param>
        /// <param name="searchValue">gia tri can tim (de rong neu lay toan bo)</param>
        /// <returns></returns>
        List<Shipper> List(int page, int pagesize, string searchValue);
        /// <summary>
        /// dem so luong nha vận chuyển
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// Lay thông tin của 1 nhà vận chuyển
        /// </summary>
        /// <param name="shipperId"></param>
        /// <returns></returns>
        Shipper Get(int shipperId);

        /// <summary>
        /// bổ sung 1 nhà vận chuyển, hàm trả về mã của nhà vận chuyển đc bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Shipper data);

        /// <summary>
        /// cập nhật thông tin
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Shipper data);

        /// <summary>
        /// xóa một nhà vận chuyển dựa vào mã
        /// </summary>
        /// <param name="shipperId"></param>
        /// <returns></returns> 
        bool Delete(int shipperId);
    }
}
