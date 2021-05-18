using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public static class SelectListHelper
    {
        /// <summary>
        /// danh sach quoc gia
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in DataService.ListCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName,
                });
            }
            return list;
        }


        public static List<SelectListItem> Cities()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in DataService.ListCities())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CityName,
                    Text = item.CityName,
                });
            }
            return list;
        }
        public static List<SelectListItem> ListSupplier()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in DataService.ListSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.SupplierID.ToString(),
                    Text = item.SupplierName,
                });
            }
            return list;
        }


        public static List<SelectListItem> ListCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in DataService.ListCategory())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CategoryID.ToString(),
                    Text = item.CategoryName,
                });
            }
            return list;
        }


        public static List<SelectListItem> ParentCategories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "Không thuộc mặt hàng nào"
            });
            foreach (var item in DataService.ListParentCategories())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.CategoryID),
                    Text = item.CategoryName
                });
            }
            return list;
        }


    }
}