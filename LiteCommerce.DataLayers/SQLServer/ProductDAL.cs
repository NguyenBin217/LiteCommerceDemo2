using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using LiteCommerce.DomainModels;
/*Hoàn thiện product DAL*/

namespace LiteCommerce.DataLayers.SQLServer
{
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        public ProductDAL(string connectionString) : base(connectionString)
        {
        }
        
        public List<Product> List(int page, int pageSize, int categoryID, int supplierID, string searchValue)
        {
            List<Product> data = new List<Product>();
            using(SqlConnection connection= GetConnection ())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT*
                                    FROM
                                    (
                                        SELECT *, ROW_NUMBER() OVER(ORDER BY ProductName) AS RowNumber
                                        FROM Products
                                        WHERE(@CategoryID = 0 OR CategoryID = @CategoryID)
                                        AND(@supplierID = 0 OR SupplierID = @supplierID)
                                        AND(@searchValue = '' OR ProductName LIKE @searchValue)
                                    ) AS s
                                    WHERE s.RowNumber BETWEEN(@page -1)*@pageSize + 1 AND @page*@pageSize";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Connection = connection;




                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Product()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            
                            Unit = Convert.ToString(dbReader["Unit"]),                           
                            Price = Convert.ToDecimal(dbReader["Price"]),
                            Photo = Convert.ToString(dbReader["Photo"])
                        });
                    }
                }
               
                connection.Close();

            }
            return data;

        }
        public int Count(int categoryID, int supplierID, string searchValue)
        {
            int count = 0;
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT COUNT(*) FROM    Products  WHERE(@categoryId = 0 OR CategoryId = @categoryId) AND(@supplierId = 0 OR SupplierId = @supplierId) AND(@searchValue = '' OR ProductName LIKE @searchValue)";

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                cmd.Parameters.AddWithValue("@supplierID", supplierID);
                cmd.Parameters.AddWithValue("@searchvalue", searchValue);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();
            }
            return count;
        }




        public Product Get(int ProductID)
        {
            Product data = null;
            using (SqlConnection connection = GetConnection())
            {
                
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Products WHERE ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Product()
                        {
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            ProductName = Convert.ToString(dbReader["ProductName"]),     
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),             
                            CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                            Price = Convert.ToDecimal(dbReader["Price"]),
                            Unit = Convert.ToString(dbReader["Unit"]),                        
                            Photo = Convert.ToString(dbReader["Photo"])
                        };
                    }
                }
                connection.Close();
            }
            return data;
        }



        public ProductEx GetEx(int productID)
        {
            Product product = Get(productID);
            if( product== null)
            {
                return null;
            }    
            List<ProductAttribute> listAttributes = this.ListAttributes(productID);
            List<ProductGallery> listGallery = this.ListGallery(productID);
            ProductEx data = new ProductEx()
            {
                ProductID= product.ProductID,
                CategoryID= product.CategoryID,
                Photo= product.Photo,
                Price= product.Price,
                ProductName= product.ProductName,
                SupplierID= product.SupplierID,
                Unit= product.Unit,
                Attributes = listAttributes,
                Galleries = listGallery
            };

            return data;
        }



        public int Add(Product data)
        {
            int productID = 0;
            using (SqlConnection connection = GetConnection())
            {
                
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Products
                                          (
                                              ProductName,
                                              SupplierID,
                                              CategoryID,                                             
                                              Unit,
                                              Price,
                                              Photo
                                          )
                                          VALUES
                                          (
	                                          @ProductName,
	                                          @SupplierID,
	                                          @CategoryID,	                                          
	                                          @Unit,@Price,                                        
	                                          @Photo
                                          );
                                          SELECT @@IDENTITY;";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);               
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                productID = Convert.ToInt32(cmd.ExecuteScalar());

                connection.Close();
            }
            return productID;
        }

        public bool Update(Product data)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = GetConnection())
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Products
                                    SET 
                                          ProductName =  @ProductName,
	                                      SupplierID =  @SupplierID,
	                                      CategoryID = @CategoryID,
	                                      Unit =  @Unit,
	                                      Price=  @Price,	                                      
	                                      Photo = @Photo
                                    WHERE ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                rowsAffected = Convert.ToInt32(cmd.ExecuteNonQuery());
                connection.Close();
            }
            return rowsAffected > 0;
        }


        public bool Delete(int productID)
        {
            bool result = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE From ProductAttributes where ProductID = @ProductID 
                                                                  AND NOT EXISTS(SELECT * FROM OrderDetails where ProductID = @ProductID)
                                    DELETE From ProductGallery where ProductID = @ProductID 
                                                                  AND NOT EXISTS(SELECT * FROM OrderDetails where ProductID = @ProductID)
                                    DELETE FROM Products
                                            WHERE(ProductID = @ProductID)
                                            AND NOT EXISTS(SELECT * FROM OrderDetails where ProductID = @ProductID)";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID",productID);
                result = cmd.ExecuteNonQuery() > 0;

                connection.Close();
            }

            return result;
        }

        public List<ProductAttribute> ListAttributes(int productID)
        {

            List<ProductAttribute> data = new List<ProductAttribute>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * 
                                    FROM ProductAttributes WHERE ProductID = @productId
                                    Order by DisplayOrder asc";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@productId", productID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new ProductAttribute()
                        {
                            AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            AttributeName = Convert.ToString(dbReader["AttributeName"]),
                            AttributeValue = Convert.ToString(dbReader["AttributeValue"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                        });
                    }
                }
                cn.Close();
            }

            return data;


        }


        public ProductAttribute GetAttribute(long attributeID)
        {
            ProductAttribute data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * 
                                    FROM ProductAttributes WHERE AttributeID = @attributeId";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@attributeId", attributeID);
                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new ProductAttribute()
                    {
                        AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        AttributeName = Convert.ToString(dbReader["AttributeName"]),
                        AttributeValue = Convert.ToString(dbReader["AttributeValue"]),
                        DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"])
                    };
                }

                cn.Close();
            }
            return data;
        }

        public long AddAttribute(ProductAttribute data)
        {
            int AttributeID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO ProductAttributes( ProductID, AttributeName, AttributeValue, DisplayOrder)
                                    VALUES (@ProductID, @AttributeName, @AttributeValue, @DisplayOrder)
                                    SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                AttributeID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return AttributeID;
        }

        public bool UpdateAttribute(ProductAttribute data)
        {

            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE ProductAttributes 
                                    SET ProductID = @ProductID, 
                                        AttributeName = @AttributeName,
                                        AttributeValue = @AttributeValue,
                                        DisplayOrder = @DisplayOrder
                                   WHERE AttributeID = @AttributeID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@AttributeID", data.AttributeID);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }


        public bool DeleteAttribute(long attributeID)
        {
            bool result = false;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"DELETE FROM ProductAttributes WHERE AttributeID = @AttributeID";

                cmd.Parameters.AddWithValue("@AttributeID", attributeID);

                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }

            return result;
        }

        public List<ProductGallery> ListGallery(int productID)
        {

            List<ProductGallery> data = new List<ProductGallery>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * 
                                    FROM ProductGallery WHERE ProductID = @productId order by DisplayOrder asc";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@productId", productID);
                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new ProductGallery()
                        {
                            GalleryID = Convert.ToInt32(dbReader["GalleryID"]),
                            ProductID = Convert.ToInt32(dbReader["ProductID"]),
                            Photo = Convert.ToString(dbReader["Photo"]),
                            Description = Convert.ToString(dbReader["Description"]),
                            DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                            IsHidden = Convert.ToInt32(dbReader["IsHidden"])
                        });
                    }
                }
                cn.Close();
            }
            return data;
        }

        public ProductGallery GetGallery(long galleryID)
        {
            ProductGallery data = null;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * 
                                    FROM ProductGallery WHERE GalleryID = @galleryId";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@galleryId", galleryID);
                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new ProductGallery()
                    {
                        GalleryID = Convert.ToInt32(dbReader["GalleryID"]),
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        Description = Convert.ToString(dbReader["Description"]),
                        DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                        IsHidden = Convert.ToInt32(dbReader["IsHidden"])
                    };
                }

                cn.Close();

            }
            return data;
        }

        public long AddGallery(ProductGallery data)
        {
            long GalleryID = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO ProductGallery( ProductID, Photo, Description, DisplayOrder, IsHidden)
                                    VALUES (@ProductID, @Photo, @Description, @DisplayOrder, @IsHidden)
                                    SELECT @@IDENTITY";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);
                GalleryID = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return GalleryID;
        }

        public bool UpdateGallery(ProductGallery data)
        {
            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE ProductGallery 
                                    SET ProductID = @ProductID, 
                                        Photo = @Photo,
                                        Description = @Description,
                                        DisplayOrder = @DisplayOrder,
                                        IsHidden = @IsHidden
                                   WHERE GalleryID = @GalleryID";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@GalleryID", data.GalleryID);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);
                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }

        public bool DeleteGallery(long galleryID)
        {

            int result = 0;
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM ProductGallery WHERE GalleryID = @galleryId";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@galleryId", galleryID);
                result = cmd.ExecuteNonQuery();
                cn.Close();
            }
            if (result == 0) return false;
            else return true;
        }

/*        int IProductDAL.Count(int page, int pageSize, int categoryID, int supplierID, string searchValue)
        {
            throw new NotImplementedException();
        }*/
    }
}


