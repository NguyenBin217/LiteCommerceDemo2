using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
using System.Data.SqlClient;

namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các tính năng liên quan đến tài khoản của Nhân viên (employee)
    /// </summary>
    public class EmployeeAcountDAL : _BaseDAL, IAccountDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeAcountDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Account Authorize(string userName, string password)
        {
            Account data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT EmployeeID, FirstName, LastName, Email, Photo FROM Employees WHERE Email = @email AND Password = @password";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@email", userName);
                cmd.Parameters.AddWithValue("@password", password);

                using (SqlDataReader dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Account()
                        {
                            AccountID = dbReader["EmployeeID"].ToString(),
                            FullName = $"{dbReader["FirstName"]} {dbReader["LastName"]}",
                            Email = dbReader["Email"].ToString(),
                            Photo = dbReader["Photo"].ToString()
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
        /// <param name="accountId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ChangePassword(string accountId, string oldPassword, string newPassword)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"update Employees
                                    set Password = @newPassword
                                    where EmployeeID = @accountId AND Password = @oldPassword";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@accountId", accountId);
                cmd.Parameters.AddWithValue("@oldPassword", oldPassword);
                cmd.Parameters.AddWithValue("@newPassword", newPassword);


                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }

            return result;
        }

        public string EmailExist(string email)
        {
            string result = "";

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT Email FROM Employees
                                    WHERE Email = @Email";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Connection = connection;
                result = Convert.ToString(cmd.ExecuteScalar());
                connection.Close();
            }
            return result;
        }

        public int Register(string firstName, string lastName, string email, string password, string photo)
        {
            int result = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO Employees
                                    (
	                                    LastName,
	                                    FirstName,
	                                    Email,
                                        Password,
                                        Photo
                                    )
                                    VALUES
                                    (
	                                    @LastName,
	                                    @FirstName,
	                                    @Email,
                                        @Password,
                                        @Photo
                                    )

                                    SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Photo", photo);
                cmd.CommandType = System.Data.CommandType.Text;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return result;
        }

    }
}
