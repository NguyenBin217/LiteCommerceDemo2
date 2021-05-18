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
    public class CategorieDAL : _BaseDAL, ICategorieDAL
    {
        public CategorieDAL(string connectionString) : base(connectionString)
        {

        }

        public List<Categorie> List()
        {
            List<Categorie> data = new List<Categorie>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select * from Categories";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Categorie()
                        {
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            ParentCategoryId = 0,
                            /*ParentCategoryId = Convert.ToInt32(dbReader["ParentCategoryId"]),*/
                        }) ;
                    }
                }
                connection.Close();
            }
            return data;
        }

        public Categorie Get(int categoryID)
        {
            Categorie data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Categories WHERE CategoryID = @categoryID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    int PCId = 0;
                    if (dbReader.IsDBNull(3)) PCId = PCId;
                    else PCId = Convert.ToInt32(dbReader["ParentCategoryId"]);
                        /*string parentCategoryId = Convert.ToString(dbReader["ParentCategoryId"]);
                        if (!String.IsNullOrWhiteSpace(parentCategoryId)) PCId = Convert.ToInt32(parentCategoryId);*/
                        data = new Categorie()
                    {
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        CategoryName = Convert.ToString(dbReader["CategoryName"]),
                        Description = Convert.ToString(dbReader["Description"]),
                        ParentCategoryId = PCId,
                    };
                }
                cn.Close();
            }
            return data;
        }
        public int Add(Categorie data)
        {
            int categorieId = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = @"INSERT INTO Categories
                                          (
	                                          CategoryName,
	                                          Description,
                                              ParentCategoryId
	                                       
                                          )
                                          VALUES
                                          (
	                                          @CategoryName,
	                                          @Description,
                                              @ParentCategoryId
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@ParentCategoryId", data.ParentCategoryId);

                categorieId = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }
            return categorieId;
        }

        public int Count(string searchValue)
        {
            int result = 0;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"select count(*) from Categories where (@SearchValue ='') or (CategoryName Like @SearchValue)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                result = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return result;
        }

        public bool Delete(int categorieId)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM Categories

                                    WHERE CategoryId = @CategoryId
                                        AND NOT EXISTS(SELECT * FROM Products where CategoryID = Categories.CategoryID)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@CategoryId", categorieId);

                int rowsAffected = cmd.ExecuteNonQuery();
                result = rowsAffected > 0;
                connection.Close();
            }
            return result;
        }

        List<Categorie> ICategorieDAL.ListParentCategories()
        {
            List<Categorie> data = new List<Categorie>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT CategoryID, CategoryName FROM Categories";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {

                        data.Add(new Categorie()
                        {
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"])
                        });
                    }
                }
                cn.Close();
            }
            return data;
        }


  
/*
        public Categorie Get(int categorieId)
        {
            *//*  Categorie data = null;*//*
            Categorie data = null;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Categories WHERE CategoryID = @CategoryID";
                cmd.Parameters.AddWithValue("@CategoryID", categorieId);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Categorie()
                        {
                            CategoryID = Convert.ToString(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            ParentCategoryId = Convert.ToString(dbReader["ParentCategoryId"]),
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }
*/
        public List<Categorie> List(int page, int pagesize, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            List<Categorie> data = new List<Categorie>();
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select * from
                                    (
                                        select *, ROW_NUMBER() OVER(ORDER by CategoryID) as RowNumber

                                        from Categories

                                        where (@searchValue = '')

                                            OR(
                                                CategoryName LIKE @searchValue

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
                        var Value = Convert.ToString(dbReader["ParentCategoryId"]); 

                        // lấy dc rồi này ! , xử lý tiếp đi hey ,,  t về nhà đã ,o kleỗi la do m ko lấy ra, m set0 maf  
                        // chừ m set lại đi lấy ra dc rồi đó !
                        data.Add(new Categorie()
                        {
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            CategoryName = Convert.ToString(dbReader["CategoryName"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            ParentCategoryId = 0,

                        });
                    }
                }
                connection.Close();
            }
            return data;
        }

        public bool Update(Categorie data)
        {
            bool result = false;
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Categories
                                           SET CategoryName = @CategoryName, 
                                           Description = @Description,
                                           ParentCategoryId = @ParentCategoryId
                                              
                                          WHERE CategoryID = @CategoryID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;

                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@CategoryName", data.CategoryName);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                if(data.ParentCategoryId != null)
                {
                    cmd.Parameters.AddWithValue("@ParentCategoryId", data.ParentCategoryId);
                } else
                {
                    cmd.Parameters.AddWithValue("@ParentCategoryId", DBNull.Value);
                }
                 
                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return result;
        }

      
    }
}
