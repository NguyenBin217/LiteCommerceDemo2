using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class OrderDAL : _BaseDAL, IOrderDAL
    {
        public OrderDAL(string connectionString) : base(connectionString)
        {
        }
        public List<Order> Order_List(int page, int pageSize, string searchValue)
        {
            List<Order> data = new List<Order>();
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = "%" + searchValue + "%";
            }
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT * FROM 
                                            ( SELECT *, ROW_NUMBER() over(order by OrderID) as RowNumber
                                              FROM View_Orders
                                              WHERE (@searchValue=N'')
                                               OR(FirstName like @searchValue) OR (LastName like @searchValue) 
                                               OR (Description like @searchValue) OR (OrderTime BETWEEN @searchValue and @searchValue)
                                        ) as T
                                        where  (t.RowNumber between (@page*@pageSize)-@pageSize+1 and @page*@pageSize)";// chuỗi câu lệnh thực thi
                    cmd.CommandType = CommandType.Text; // kiểu câu lệnh procedu text 
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);
                    using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dbReader.Read())
                        {
                            data.Add(new Order()
                            {
                                OrderID = Convert.ToInt32(dbReader["OrderID"]),
                                CustomerID = Convert.ToString(dbReader["CustomerID"]),
                                EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                                CustomerName = Convert.ToString(dbReader["ContactName"]),
                                EmployeeName = Convert.ToString(dbReader["FirstName"]) + " " + Convert.ToString(dbReader["LastName"]),
                                ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                                ShipperName = Convert.ToString(dbReader["CompanyName"])
                            });
                        }
                    }

                }
                connection.Close();
            }
            return data;
        }
        public int Count_Order(string searchValue)
        {
            throw new NotImplementedException();
        }

        public bool Delete_Order(int[] orderIDs)
        {
            throw new NotImplementedException();
        }

        public List<OrderDetail> OrderDetail_List()
        {
            throw new NotImplementedException();
        }

        public Product Get(int OrderID)
        {
            throw new NotImplementedException();
        }
    }
}
