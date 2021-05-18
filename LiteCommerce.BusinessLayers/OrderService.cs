using LiteCommerce.DataLayers;
using LiteCommerce.DataLayers.SQLServer;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    public static class OrderService
    {
        /// <summary>
        /// 
        /// </summary>
        private static IOrderDAL OrderDB { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            OrderDB = new OrderDAL(connectionString);
        }
        /// <summary>
        /// trả về danh sách đơn hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Order> Order_List(int page, int pageSize, int category, int supplier, string searchValue, out int rowCount)
        {
            rowCount = OrderDB.Count_Order(searchValue);
            return OrderDB.Order_List(page, pageSize, searchValue);
        }
        // <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static Order Get(int orderId)
        {
            return OrderDB.Get(orderId);
        }
        /// <summary>
        /// trả về danh sách chi tiết đơn hàng
        /// </summary>
        /// <returns></returns>
        public static List<OrderDetail> OrderDetail_List()
        {
            return OrderDB.OrderDetail_List();
        }
        /// <summary>
        /// Xóa nhiều đơn hàng 
        /// </summary>
        /// <param name="orderIDs"></param>
        /// <returns></returns>
        public static bool Delete_Order(int[] orderIDs)
        {
            return OrderDB.Delete_Order(orderIDs);
        }
    }
}