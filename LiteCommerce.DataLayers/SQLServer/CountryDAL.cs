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
    /// <summary>
    /// 
    /// </summary>
    public class CountryDAL : _BaseDAL, ICountryDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CountryDAL(string connectionString) : base (connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Country> List()
        {
            List<Country> data = new List<Country>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Countries";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Country()
                        {
                            CountryName = Convert.ToString(dbReader["CountryName"])
                        });
                    }
                }
                connection.Close();
            }
            return data;
        }
    }
}
