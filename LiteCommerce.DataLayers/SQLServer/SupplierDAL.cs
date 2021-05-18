
using LiteCommerce.DataLayers.SQLServer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class SupplierDAL : _BaseDAL, ISupplierDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public SupplierDAL(string connectionString) : base(connectionString)
        {

        }

        /// <summary>
        /// Hiển thị danh sách nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public List<Supplier> List()
        {
            List<Supplier> data = new List<Supplier>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from Suppliers";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Supplier()
                        {
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            SupplierName = Convert.ToString(dbReader["SupplierName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Phone = Convert.ToString(dbReader["Phone"])
                        });
                    }
                }
                connection.Close();
            }
            return data;
        }


        /// <summary>
        /// Thêm nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Supplier data)
        {
            int supplierId = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Suppliers
                                    (
	                                    SupplierName,
	                                    ContactName,
	                                    Address,
	                                    City,
	                                    PostalCode,
	                                    Country,
	                                    Phone
                                    )
                                    VALUES
                                    (
	                                    @SupplierName,
	                                    @ContactName,
	                                    @Address,
	                                    @City,
	                                    @PostalCode,
	                                    @Country,
	                                    @Phone
                                    )
                                    SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@SupplierName", data.SupplierName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);


                supplierId = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return supplierId;
        }


        /// <summary>
        /// đếm số lượng nhà cung cấp
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            int result = 0;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT COUNT(*)
                                    FROM Suppliers
                                    WHERE ContactName LIKE @searchValue
                                    OR SupplierName LIKE @searchValue";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");
                cmd.Connection = connection;
                result = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return result;
        }


        /// <summary>
        /// Xóa nhà cung cấp theo mã id
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public bool Delete(int supplierId)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM Suppliers WHERE SupplierID = @supplierID
                                   AND NOT EXISTS ( SELECT * FROM Products WHERE SupplierID = Suppliers.SupplierID)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@supplierID", supplierId);
                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }


        /// <summary>
        /// Lấy thông tin nhà cung cấp thep mã
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public Supplier Get(int supplierId)
        {
            Supplier data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Suppliers WHERE SupplierID = @SupplierID";
                cmd.Parameters.AddWithValue("@SupplierID", supplierId);
                cmd.CommandType = System.Data.CommandType.Text;

                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Supplier()
                        {
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            SupplierName = Convert.ToString(dbReader["SupplierName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Phone = Convert.ToString(dbReader["Phone"])
                        };
                    }
                }

                connection.Close();
            }

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">Trang cần lấy dữ liệu</param>
        /// <param name="pageSize">số dòng trên mỗi trang</param>
        /// <param name="searchValue">Giá trị cần tìm(để rỗng nếu lấy toàn bộ)</param>
        /// <returns></returns>
        public List<Supplier> List(int page, int pageSize, string searchValue)
        {
            List<Supplier> data = new List<Supplier>();
            using (SqlConnection cn = GetConnection())
            {
                if (searchValue != "")
                    searchValue = "%" + searchValue + "%";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM
                                        (
                                            SELECT *, ROW_NUMBER() OVER(ORDER BY SupplierName) AS RowNumber
                                            FROM Suppliers
                                            WHERE (@searchValue='')
	                                            OR(
		                                            SupplierName LIKE @searchValue
		                                            Or ContactName LIKE @searchValue
		                                            Or Address LIKE @searchValue
		                                            Or Phone LIKE @searchValue
	                                                )
                                          ) AS s
                                     WHERE s.RowNumber BETWEEN (@page - 1)*@pageSize + 1 AND @page*@pageSize";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Supplier()
                        {

                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            SupplierName = Convert.ToString(dbReader["SupplierName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Phone = Convert.ToString(dbReader["Phone"]),
                        });
                    }
                }
                cn.Close();
            }
            return data;
        }

        /// <summary>
        /// Cập nhật nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Supplier data)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Suppliers
                                    SET SupplierName = @SupplierName,
	                                    ContactName = @ContactName,
	                                    Address = @Address,
	                                    City = @City,
	                                    PostalCode = @PostalCode,
	                                    Country = @Country,
	                                    Phone = @Phone
                                    WHERE SupplierID = @SupplierID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@SupplierName", data.SupplierName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);

                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return result;
        }

     
    }
}