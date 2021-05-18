using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Khai bao cac chuc nang xu ly du lieu lien quan den nhân viên
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Lay danh sach nha cung cap
        /// </summary>
        /// <param name="page">trang can lay du lieu</param>
        /// <param name="pagesize">so dong tren moi trang</param>
        /// <param name="searchValue">gia tri can tim (de rong neu lay toan bo)</param>
        /// <returns></returns>
        List<Employee> List(int page, int pagesize, string searchValue);
        /// <summary>
        /// dem so luong nha cung cap
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// Lay thông tin của 1 nhà cung cấp
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Employee Get(int employeeId);

        /// <summary>
        /// bổ sung 1 nhà cung cấp, hàm trả về mã của nhà cung cấp đc bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Employee data);

        /// <summary>
        /// cập nhật thông tin
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Employee data);

        /// <summary>
        /// xóa một nhà cung cấp dựa vào mã
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns> 
        bool Delete(int employeeId);
    }
}
