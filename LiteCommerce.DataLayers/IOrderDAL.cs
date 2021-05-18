using LiteCommerce.DataLayers.SQLServer;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IOrderDAL
    {
        /// <summary>
        /// Lấy danh sách đơn hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Order> Order_List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Lấy thông tin mặt hàng theo mã 
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        Product Get(int OrderID);
        /// <summary>
        /// Đếm số lượng đơn hàng
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count_Order(string searchValue);
        /// <summary>
        /// lấy dánh sách chi tiết đơn hàng
        /// </summary>
        /// <returns></returns>
        List<OrderDetail> OrderDetail_List();
        /// <summary>
        /// Xóa đơn hàng sẽ xóa luôn chi tiết đơn hàng đó
        /// </summary>
        /// <param name="orderIDs"></param>
        /// <returns></returns>
        bool Delete_Order(int[] orderIDs);
    }
}