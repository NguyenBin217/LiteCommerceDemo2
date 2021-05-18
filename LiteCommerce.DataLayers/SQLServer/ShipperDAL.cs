using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class ShipperDAL : _BaseDAL, IShipperDAL
    {
        public ShipperDAL(string connectionString) : base(connectionString)
        {

        }
        public int Add(Shipper data)
        {
            int ShipperId = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Shippers
                                    (
	                                    ShipperName,
	                                    Phone
                                    )
                                    VALUES
                                    (
	                                    @ShipperName,
	                                    @Phone
                                    )
                                    SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);

                ShipperId = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }
            return ShipperId;
        }

        public int Count(string searchValue)
        {
            int result = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select count(*) from Shippers where (@SearchValue ='') or (ShipperName Like @SearchValue)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                result = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return result;
        }

        public bool Delete(int shipperId)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM Shippers
                                            WHERE(ShipperId = @ShipperId)
                                              AND(ShipperID NOT IN(SELECT ShipperID FROM Orders))";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ShipperId", shipperId);

                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;
                connection.Close();
            }
            return result;
        }

        public Shipper Get(int shipperId)
        {
            Shipper data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Shippers WHERE ShipperID = @ShipperID";
                cmd.Parameters.AddWithValue("@ShipperID", shipperId);
                cmd.CommandType = System.Data.CommandType.Text;
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Shipper()
                        {
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            ShipperName = Convert.ToString(dbReader["ShipperName"]),
                            Phone = Convert.ToString(dbReader["Phone"])
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }

        public List<Shipper> List(int page, int pagesize, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            List<Shipper> data = new List<Shipper>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from
                                    (
                                        select *, ROW_NUMBER() OVER(ORDER by ShipperID) as RowNumber

                                        from Shippers

                                        where (@searchValue = '')

                                            OR(
                                                ShipperName LIKE @searchValue

                                            )
                                    ) AS s
                                    where s.RowNumber between(@page -1)*@pageSize + 1 and @page*@pageSize
                                    Order by s.RowNumber";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pagesize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Connection = connection;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Shipper()
                        {
                            ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                            ShipperName = Convert.ToString(dbReader["ShipperName"]),
                            Phone = Convert.ToString(dbReader["Phone"])
                        });
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool Update(Shipper data)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Shippers
                                            SET ShipperName = @ShipperName,
                                            Phone = @Phone
                                    WHERE ShipperID = @ShipperID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperID);
                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);

                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return result;
        }
    }
}
