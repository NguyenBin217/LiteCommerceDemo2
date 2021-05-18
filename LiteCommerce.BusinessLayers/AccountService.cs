using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
using LiteCommerce.DataLayers;
namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// Cung cấp các chức năng liên quan đến tài khoản người dùng
    /// </summary>
    public static class AccountService
    {
        private static IAccountDAL AccountDB;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectionString"></param>
        /// <param name="accountType"></param>
        public static void Init(DatabaseTypes dbType, string connectionString, AccountTypes accountType)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    if (accountType == AccountTypes.Employee)
                        AccountDB = new DataLayers.SQLServer.EmployeeAcountDAL(connectionString);
                    else
                        AccountDB = new DataLayers.SQLServer.CustomerAccountDAL(connectionString);
                    break;

                default:
                    throw new Exception("Database Type is not supported");
            }
        }

        /// <summary>
        /// Kiểm tra thông tin đăng nhập cảu tài khoản
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static Account Authorize(string userName, string password)
        {
            return AccountDB.Authorize(userName, password);
        }

        public static bool ChangePassword(string accountId, string oldPassword, string newPassword)
        {
            return AccountDB.ChangePassword(accountId, oldPassword, newPassword);
        }

        public static int Register(string firstName, string lastName, string email, string password, string photo)
        {
            return AccountDB.Register(firstName, lastName, email, password, photo);
        }

        public static string EmailExist(string email)
        {
            return AccountDB.EmailExist(email);
        }
    }

    /// <summary>
    /// Loại người dùng
    /// </summary>
    public enum AccountTypes
    {
        /// <summary>
        /// Nhân viên (dùng ứng dụng LiteCommerce.Admin)
        /// </summary>
        Employee,
        /// <summary>
        /// Khách hàng (dùng ứng dụng LiteCommerce.Shop)
        /// </summary>
        Customer
    }
}
