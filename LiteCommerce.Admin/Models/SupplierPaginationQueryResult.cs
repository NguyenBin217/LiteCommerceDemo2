using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class SupplierPaginationQueryResult : BasePaginationQueryResult
    {
        public List<Supplier> Data { get; set; }
    }

    public class CustomerPaginationQueryResult : BasePaginationQueryResult
    {
        public List<Customer> Data { get; set; }
    }

    public class CategoriePaginationQueryResult : BasePaginationQueryResult
    {
        public List<Categorie> Data { get; set; }
    }

    public class ShipperPaginationQueryResult : BasePaginationQueryResult
    {
        public List<Shipper> Data { get; set; }
    }

    public class EmployeePaginationQueryResult : BasePaginationQueryResult
    {
        public List<Employee> Data { get; set; }
    }
}