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
    public class CustomerDAL : _BaseDAL, ICustomerDAL
    {
        public CustomerDAL(string connectionString) : base(connectionString)
        {

        }
        public int Add(Customer data)
        {
            int customerId = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Customers
                                    (
	                                    CustomerName,
	                                    ContactName,
	                                    Address,
	                                    City,
	                                    PostalCode,
	                                    Country,
	                                    Email,
                                        Password
                                    )
                                    VALUES
                                    (
	                                    @CustomerName,
	                                    @ContactName,
	                                    @Address,
	                                    @City,
	                                    @PostalCode,
	                                    @Country,
	                                    @Email,
                                        @Password
                                    );
                                    SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                /*cmd.CommandType = System.Data.CommandType.Text;*/
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@CustomerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);

                customerId = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }
            return customerId;
        }

        public int Count(string searchValue)
        {
            int result = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select count(*) from Customers where (@SearchValue ='') or (CustomerName Like @SearchValue or ContactName Like @SearchValue)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                result = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return result;
        }

        public bool Delete(int customerId)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM Customers
                                            WHERE(CustomerID = @CustomerId)
                                              AND(CustomerID NOT IN(SELECT CustomerID FROM Orders))";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@CustomerId", customerId);

                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;
                connection.Close();
            }
            return result;
        }

        public Customer Get(int customerId)
        {
            Customer data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Customers WHERE CustomerID = @CustomerID";
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                cmd.CommandType = System.Data.CommandType.Text;
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Customer()
                        {
                            CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                            CustomerName = Convert.ToString(dbReader["CustomerName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = Convert.ToString(dbReader["Password"]),
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }

        public List<Customer> List(int page, int pageSize, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            List<Customer> data = new List<Customer>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from
                                    (
                                        select *, ROW_NUMBER() OVER(ORDER by CustomerID) as RowNumber

                                        from Customers

                                        where (@searchValue = '')

                                            OR(
                                                CustomerName LIKE @searchValue

                                                or ContactName Like @searchValue

                                                or Address like @searchValue

                                                
                                            )
                                    ) AS s
                                    where s.RowNumber between(@page -1)*@pageSize + 1 and @page*@pageSize
                                    Order by s.RowNumber";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Connection = connection;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Customer()
                        {
                            CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                            CustomerName = Convert.ToString(dbReader["CustomerName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = Convert.ToString(dbReader["Password"])
                        });
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool Update(Customer data)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Customers 
                                SET	CustomerName = @CustomerName,
	                                ContactName = @ContactName,
	                                Address = @Address,
	                                City = @City,
	                                PostalCode = @PostalCode,
                                    Country = @Country,
	                                Email = @Email,
                                    Password = @Password
                                WHERE CustomerID = @CustomerID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@CustomerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);

                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return result;
        }
    }
}
