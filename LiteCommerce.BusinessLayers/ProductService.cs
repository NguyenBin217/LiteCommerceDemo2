using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// Cung cấp các chức năng tác nghiệp liên quan đến hàng hóa
    /// </summary>
    public static class ProductService
    {
        private static IProductDAL ProductDB;

        /// <summary>
        /// Khởi tạo tính năng tác nghiệp (hàm này phải được gọi nếu muốn sử dụng
        /// các tính năng của lớp)
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectionString"></param>
        public static void Init(DatabaseTypes dbType, string connectionString)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    ProductDB = new DataLayers.SQLServer.ProductDAL(connectionString);
                    break;
                default:
                    throw new Exception("Database type is not supported");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="categoryId"></param>
        /// <param name="supplierrId"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Product> List(int page, int pageSize,
                                int categoryId, int supplierId,
                                string searchValue, out int rowCount)
        {
            rowCount = ProductDB.Count(categoryId, supplierId, searchValue);
            return ProductDB.List(page, pageSize, categoryId, supplierId, searchValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static Product Get(int productId)
        {
            return ProductDB.Get(productId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static ProductEx GetEx(int productId)
        {
            return ProductDB.GetEx(productId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Add(Product data)
        {
            return ProductDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Update(Product data)
        {
            return ProductDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static bool Delete(int productId)
        {
            return ProductDB.Delete(productId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static List<ProductAttribute> ListAttributes(int productId)
        {
            return ProductDB.ListAttributes(productId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeId"></param>
        /// <returns></returns>
        public static ProductAttribute GetAttribute(long attributeId)
        {
            return ProductDB.GetAttribute(attributeId);
        }

    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long AddAttribute(ProductAttribute data)
        {
            return ProductDB.AddAttribute(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateAttribute(ProductAttribute data)
        {
            return ProductDB.UpdateAttribute(data);
        }
        /// <summary>
        /// Xóa (nhiều) attribute
        /// </summary>
        /// <param name="attributeIds"></param>
        public static void DeleteAttributes(long[] attributeIds)
        {
            foreach (var id in attributeIds)
            {
                ProductDB.DeleteAttribute(id);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static List<ProductGallery> ListGalleries(int productId)
        {
            return ProductDB.ListGallery(productId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryId"></param>
        /// <returns></returns>
        public static ProductGallery GetGallery(long galleryId)
        {
            return ProductDB.GetGallery(galleryId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static long AddGallery(ProductGallery data)
        {
            return ProductDB.AddGallery(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateGallery(ProductGallery data)
        {
            return ProductDB.UpdateGallery(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="galleryIds"></param>
        public static void DeleteGalleries(long[] galleryIds)
        {
            foreach (var id in galleryIds)
            {
                ProductDB.DeleteGallery(id);
            }
        }

    }
}