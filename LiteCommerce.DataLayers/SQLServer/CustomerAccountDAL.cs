using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;

namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các tính năng liên quan đến tài khoản của Nhân viên (employee)    /// </summary>
    public class CustomerAccountDAL : _BaseDAL, IAccountDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CustomerAccountDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Account Authorize(string userName, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public bool ChangePassword(string accountId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public string EmailExist(string email)
        {
            throw new NotImplementedException();
        }

        public int Register(string firstName, string lastName, string email, string password, string photo)
        {
            throw new NotImplementedException();
        }
    }

}
