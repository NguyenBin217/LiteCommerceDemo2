using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    public class CityDAL : _BaseDAL, ICityDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CityDAL(string connectionString) : base(connectionString)
        {

        }

        public List<City> List()
        {
            List<City> data = new List<City>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from Cities";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new City()
                        {
                            CityName = Convert.ToString(dbReader["CityName"]),
                            CountryName = Convert.ToString(dbReader["CountryName"])
                        });
                    }
                }
                connection.Close();
            }
            return data;
        }

        public List<City> List(string countryName)
        {
            List<City> data = new List<City>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from Cities WHERE CountryName = @CountryName";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CountryName", countryName);
                cmd.Connection = connection;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new City()
                        {
                            CityName = Convert.ToString(dbReader["CityName"]),
                            CountryName = Convert.ToString(dbReader["CountryName"])
                        });
                    }
                }
                connection.Close();
            }
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

    }
}