using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DataLayers;
using LiteCommerce.DataLayers.SQLServer;
using LiteCommerce.DomainModels;

namespace LiteCommerce.BusinessLayers
{
    public static class DataService
    {
        private static ICountryDAL CountryDB;
        private static IProductDAL ProductDB;
        private static ICityDAL CityDB;
        private static ISupplierDAL SupplierDB;
        private static ICustomerDAL CustomerDB;
        private static ICategorieDAL CategorieDB;
        private static IShipperDAL ShipperDB;
        private static IEmployeeDAL EmployeeDB;
        
        /// <summary>
        /// Khởi tạo lớp tác nghiệp
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectString"></param>
        public static void Init(DatabaseTypes dbType, string connectString)
        {
            switch (dbType)
            {
                case DatabaseTypes.SQLServer:
                    CountryDB = new DataLayers.SQLServer.CountryDAL(connectString);
                    CityDB = new DataLayers.SQLServer.CityDAL(connectString);
                    SupplierDB = new DataLayers.SQLServer.SupplierDAL(connectString);
                    CustomerDB = new DataLayers.SQLServer.CustomerDAL(connectString);
                    CategorieDB = new DataLayers.SQLServer.CategorieDAL(connectString);
                    ShipperDB = new DataLayers.SQLServer.ShipperDAL(connectString);
                    EmployeeDB = new DataLayers.SQLServer.EmployeeDAL(connectString);
                    ProductDB = new DataLayers.SQLServer.ProductDAL(connectString);
                    break;
                case DatabaseTypes.FakeDB:

                    break;

                default:
                    throw new Exception("Database Type is not supported");
            }
        }

        public static Categorie GetCategory(int categoryId)
        {
            return CategorieDB.Get(categoryId);
        }

        public static bool DeleteProduct(int id)
        {
            return ProductDB.Delete(id);
        }

        public static object GetProduct(int id)
        {
            return ProductDB.Get(id);
        }

        public static bool UpdateProduct(Product data)
        {
            return ProductDB.Update(data);
        }

        public static int AddProduct(Product data)
        {
            return ProductDB.Add(data);
        }

        public static List<Categorie> ListParentCategories()
        {
            return CategorieDB.ListParentCategories();
        }

        public static List<Categorie> ListCategory()
        {
            return CategorieDB.List();
        }

        /// <summary>
        /// Lấy dnah sách các quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListCountries()
        {
            return CountryDB.List();
        }


        /// <summary>
        /// Hieenr thi danh sach tat ca thanh pho
        /// </summary>
        /// <returns></returns>
        public static List<City> ListCities()
        {
            return CityDB.List();
        } 
   

       public static List<Supplier> ListSuppliers()
        {
            return SupplierDB.List();

        }

        /// <summary>
        /// Lay danh sach cac thanh pho theo quoc gia
        /// </summary>
        /// <param name="countryName"></param>
        /// <returns></returns>
        public static List<City> ListCities(string countryName)
        {
            return CityDB.List(countryName);
        }



        public static List<Supplier> ListSuppliers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = SupplierDB.Count(searchValue);
            return SupplierDB.List(page, pageSize, searchValue);
        }

        /// <summary>
        /// Laays thong tin nha cung cap theo ma
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return SupplierDB.Get(supplierID);
        }

        public static int AddSupplier(Supplier data)
        {
            return SupplierDB.Add(data);
        }

        public static bool UpdateSupplier(Supplier data)
        {
            return SupplierDB.Update(data);
        }

        public static bool DeleteSupplier(int supplierID)
        {
            return SupplierDB.Delete(supplierID);
        }




        public static List<Customer> ListCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 25;
            }
            rowCount = CustomerDB.Count(searchValue);
            return CustomerDB.List(page, pageSize, searchValue);
        }

        public static Customer GetCustomer(int customerID)
        {
            return CustomerDB.Get(customerID);
        }

        public static int AddCustomer(Customer data)
        {
            return CustomerDB.Add(data);
        }

        public static bool UpdateCustomer(Customer data)
        {
            return CustomerDB.Update(data);
        }

        public static bool DeleteCustomer(int customerID)
        {
            return CustomerDB.Delete(customerID);
        }






        public static List<Categorie> ListCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 25;
            }
            rowCount = CategorieDB.Count(searchValue);
            return CategorieDB.List(page, pageSize, searchValue);
        }

  
        public static Categorie GetCategorie(int categorieID)
        {
            return CategorieDB.Get(categorieID);
        }

        public static int AddCategorie(Categorie data)
        {
            return CategorieDB.Add(data);
        }

        public static bool UpdateCategorie(Categorie data)
        {
            return CategorieDB.Update(data);
        }

        public static bool DeleteCategorie(int categorieID)
        {
            return CategorieDB.Delete(categorieID);
        }





        public static List<Shipper> ListShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 25;
            }
            rowCount = ShipperDB.Count(searchValue);
            return ShipperDB.List(page, pageSize, searchValue);
        }


        public static Shipper GetShipper(int shipperID)
        {
            return ShipperDB.Get(shipperID);
        }

        public static int AddShipper(Shipper data)
        {
            return ShipperDB.Add(data);
        }

        public static bool UpdateShipper(Shipper data)
        {
            return ShipperDB.Update(data);
        }

        public static bool DeleteShipper(int shipperID)
        {
            return ShipperDB.Delete(shipperID);
        }





        public static List<Employee> ListEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 25;
            }
            rowCount = EmployeeDB.Count(searchValue);
            return EmployeeDB.List(page, pageSize, searchValue);
        }


        public static Employee GetEmployee(int employeeID)
        {
            return EmployeeDB.Get(employeeID);
        }

        public static int AddEmployee(Employee data)
        {
            return EmployeeDB.Add(data);
        }

        public static bool UpdateEmployee(Employee data)
        {
            return EmployeeDB.Update(data);
        }

        public static bool DeleteEmployee(int employeeID)
        {
            return EmployeeDB.Delete(employeeID);
        }
    }
}
