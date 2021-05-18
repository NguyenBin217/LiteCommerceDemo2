using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class _BaseDAL
    {
        /// <summary>
        /// chuỗi tham số kết nối
        /// </summary>
        protected string _connectionString;

        public _BaseDAL(string connectionString)
        {
            this._connectionString = connectionString;
        }

        /// <summary>
        /// tạo và mở kết nối đến csdl
        /// </summary>
        /// <returns></returns>
        protected SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = this._connectionString;
            connection.Open();
            return connection;
        }
    }
}
