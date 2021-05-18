using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý liên quan đến tài khoản đăng nhập vào hệ thống
    /// </summary>
    public interface IAccountDAL
    {
        /// <summary>
        /// Kiểm tra thông tin đăng nhập. Nếu đăng nhập đúng thì trả về 1 đối tượng kiể Account (thông tin của tài khoản đăng nhập). Ngược lại, trả về null
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Account Authorize(string userName, string password);

        /// <summary>
        /// Thay đổi mật khẩu của tài khoản
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(string accountId, string oldPassword, string newPassword);

        int Register(string firstName, string lastName, string email, string password, string photo);

        string EmailExist(string email);
    }
}
