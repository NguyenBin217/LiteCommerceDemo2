using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;


namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai bao cac chuc nang xu ly du lieu lien quan den danh muc san pham
    /// </summary>
    public interface ICategorieDAL
    {
        /// <summary>
        /// Lay danh sach danh muc san pham
        /// </summary>
        /// <param name="page">trang can lay du lieu</param>
        /// <param name="pagesize">so dong tren moi trang</param>
        /// <param name="searchValue">gia tri can tim (de rong neu lay toan bo)</param>
        /// <returns></returns>
        List<Categorie> List(int page, int pagesize, string searchValue);
        /// <summary>
        /// dem so luong danh muc san pham
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// Lay thông tin của 1 danh muc san pham
        /// </summary>
        /// <param name="categorieId"></param>
        /// <returns></returns>
        Categorie Get(int categorieId);

        /// <summary>
        /// bổ sung 1 danh muc san pham, hàm trả về mã của danh muc san pham đc bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Categorie data);

        /// <summary>
        /// cập nhật thông tin
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Categorie data);

        /// <summary>
        /// xóa một danh muc san pham dựa vào mã
        /// </summary>
        /// <param name="categorieId"></param>
        /// <returns></returns> 
        bool Delete(int categorieId);
        List<Categorie> List();
        List<Categorie> ListParentCategories();
    }
}
