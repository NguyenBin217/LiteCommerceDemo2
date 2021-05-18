using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class RegisterDAL : _BaseDAL, IRegisterDAL
    {
        public RegisterDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Register data)
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
                                        Password
                                    )
                                    VALUES
                                    (
	                                    @LastName,
	                                    @FirstName,
	                                    @Email,
                                        @Password
                                    )

                                    SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@LastName", data.FullName);
                cmd.Parameters.AddWithValue("@FirstName", data.Photo);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", data.AccountID);
                cmd.CommandType = System.Data.CommandType.Text;

                result = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }

            return result;
        }
    }
}
