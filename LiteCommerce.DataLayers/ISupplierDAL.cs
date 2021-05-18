using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai bao cac chuc nang xu ly du lieu lien quan den nha cung cap
    /// </summary>
    public interface ISupplierDAL
     
    {
        /// <summary>
        /// Lay danh sach nha cung cap
        /// </summary>
        /// <param name="page">trang can lay du lieu</param>
        /// <param name="pagesize">so dong tren moi trang</param>
        /// <param name="searchValue">gia tri can tim (de rong neu lay toan bo)</param>
        /// <returns></returns>
        List<Supplier> List(int page, int pagesize, string searchValue);
        /// <summary>
        /// dem so luong nha cung cap
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// Lay thông tin của 1 nhà cung cấp
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        Supplier Get(int supplierId);

        /// <summary>
        /// bổ sung 1 nhà cung cấp, hàm trả về mã của nhà cung cấp đc bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Supplier data);

        /// <summary>
        /// cập nhật thông tin
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Supplier data);

        /// <summary>
        /// xóa một nhà cung cấp dựa vào mã
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns> 
        bool Delete(int supplierId);
        List<Supplier> List();
    }
}
