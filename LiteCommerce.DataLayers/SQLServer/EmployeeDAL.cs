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
    public class EmployeeDAL : _BaseDAL, IEmployeeDAL
    {
        public EmployeeDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Employee data)
        {
            int employeeId = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Employees
                                    (
	                                    LastName,
	                                    FirstName,
	                                    BirthDate,
	                                    Photo,
	                                    Notes,
	                                    Email,
	                                    Password
                                    )
                                    VALUES
                                    (
	                                    @LastName,
	                                    @FirstName,
	                                    @BirthDate,
	                                    @Photo,
	                                    @Notes,
	                                    @Email,
	                                    @Password
                                    )
                                    SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);

                employeeId = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }
            return employeeId;
        }

        public int Count(string searchValue)
        {
            int result = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select count(*) from Employees where (@SearchValue ='') or (FirstName Like @SearchValue or LastName Like @SearchValue)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                result = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return result;
        }

        public bool Delete(int employeeId)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM Employees
                                            WHERE(EmployeeID = @employeeId)
                                              AND(EmployeeID NOT IN(SELECT EmployeeID FROM Orders))";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;
                connection.Close();
            }
            return result;
        }

        public Employee Get(int employeeId)
        {
            Employee data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                cmd.CommandType = System.Data.CommandType.Text;
                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Employee()
                        {
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            LastName = Convert.ToString(dbReader["LastName"]),
                            FirstName = Convert.ToString(dbReader["FirstName"]),
                            BirthDate = Convert.ToString(dbReader["BirthDate"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Notes = Convert.ToString(dbReader["Notes"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = Convert.ToString(dbReader["Password"]),
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }

        public List<Employee> List(int page, int pagesize, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            List<Employee> data = new List<Employee>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from
                                    (
                                        select *, ROW_NUMBER() OVER(ORDER by EmployeeID) as RowNumber

                                        from Employees

                                        where (@searchValue = '')

                                            OR(
                                                FirstName LIKE @searchValue

                                                or LastName Like @searchValue

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
                        data.Add(new Employee()
                        {
                            EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                            FirstName = Convert.ToString(dbReader["FirstName"]),
                            LastName = Convert.ToString(dbReader["LastName"]),
                            BirthDate = Convert.ToString(dbReader["BirthDate"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Notes = Convert.ToString(dbReader["Notes"]),
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = Convert.ToString(dbReader["Password"])
                        });
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool Update(Employee data)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Employees 
                                SET	LastName = @LastName,
	                                FirstName = @FirstName,
	                                BirthDate = @BirthDate,
	                                Photo = @Photo,
	                                Notes = @Notes,
	                                Email = @Email,
                                    Password = @Password
                                WHERE EmployeeID = @EmployeeID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.Password);

                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return result;
        }
    }
}
