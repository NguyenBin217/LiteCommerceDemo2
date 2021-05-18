using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai baso cac chuc nang xu ly lien quan den country
    /// </summary>
    public interface IProductDAL
    {
        /// <summary>
        /// Danh sách mặt hàng(tìm kiếm lọc dữ liệu , phân trang)
        /// </summary>
        /// mã hàng laoij cần lấy (0 nếu như lấy tất cả các loại hàng )
        /// mã nhà cung cấp bằng 0 nếu lấy tất cả của nhà cung cấp 
        /// tên mặt hàng cần tìm ( chuỗi rỗng nếu không tìm kiếm )
        /// <returns></returns>
       
        List<Product> List(int page, int pageSize,int categoryID, int supplierID, string searchValue);
        /*Đếm số mặt hàng khi tìm kiếm được, rowCount*/
        int Count(int categoryID, int supplierID, string searchValue);
        /// <summary>
        /// Lấy thông tin mặt hàng theo mã 
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        Product Get(int ProductID);
        /// <summary>
        /// lay thong tin chi tiet co mo rong cac thuoc tinh va thu vien anh
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        ProductEx GetEx(int productID);

        int Add(Product data);
        bool Update(Product data);
        bool Delete(int productID);
        /// <summary>
        /// Lấy danh sách các thuoc tinh cua mot mat hang
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        List<ProductAttribute> ListAttributes(int productID);
       // int Count(int categoryId, int supplierId, string searchValue);

        ProductAttribute GetAttribute(long attributeID);
        long AddAttribute(ProductAttribute data);
        bool UpdateAttribute(ProductAttribute data);
        bool DeleteAttribute(long attributeID);
        /// <summary>
        /// ListGallery
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        List<ProductGallery> ListGallery(int productID);
        ProductGallery GetGallery(long categoryID);
        long AddGallery(ProductGallery data);
        bool UpdateGallery(ProductGallery data);
        bool DeleteGallery(long galleryID);



    }
}
