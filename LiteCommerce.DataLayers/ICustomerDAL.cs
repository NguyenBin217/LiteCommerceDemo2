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
    public interface ICustomerDAL
    {
        /// <summary>
        /// Lay danh sach khach hang
        /// </summary>
        /// <param name="page">trang can lay du lieu</param>
        /// <param name="pagesize">so dong tren moi trang</param>
        /// <param name="searchValue">gia tri can tim (de rong neu lay toan bo)</param>
        /// <returns></returns>
        List<Customer> List(int page, int pagesize, string searchValue);
        /// <summary>
        /// dem so luong nha cung cap
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// Lay thông tin của 1 khach hang
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Customer Get(int customerId);

        /// <summary>
        /// bổ sung 1 khach hang, hàm trả về mã của khach hang đc bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Customer data);

        /// <summary>
        /// cập nhật thông tin
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Customer data);

        /// <summary>
        /// xóa một khach hang dựa vào mã
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns> 
        bool Delete(int customerId);
    }
}
