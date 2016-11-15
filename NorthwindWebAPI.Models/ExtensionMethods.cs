using System;
using System.Data.SqlClient;

namespace NorthwindWebAPI.Models
{
    public static class ExtensionMethods
    {
        public static SqlParameter[] ToParameterArray(this Customer customer)
        {
            return new SqlParameter[] { new SqlParameter("@CustomerID", customer.CustomerID)
                                                , new SqlParameter("@CompanyName", customer.CompanyName)
                                                , new SqlParameter("@ContactName", customer.ContactName)
                                                , new SqlParameter("@ContactTitle", customer.ContactTitle)
                                                , new SqlParameter("@Address", customer.Address)
                                                , new SqlParameter("@City", customer.City)
                                                , new SqlParameter("@Region", customer.Region ?? (object)DBNull.Value)
                                                , new SqlParameter("@PostalCode", customer.PostalCode)
                                                , new SqlParameter("@Country", customer.Country)
                                                , new SqlParameter("@Phone", customer.Phone)
                                                , new SqlParameter("@Fax", customer.Fax ?? (object)DBNull.Value)
                                                };
        }

        public static SqlParameter[] ToParameterArray(this Employee employee)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@EmployeeID", employee.EmployeeID)
                , new SqlParameter("@LastName", employee.LastName)
                , new SqlParameter("@FirstName", employee.FirstName)
                , new SqlParameter("@Title", employee.Title ?? (object)DBNull.Value)
                , new SqlParameter("@TitleOfCourtesy", employee.TitleOfCourtesy ?? (object)DBNull.Value)
                , new SqlParameter("@BirthDate", employee.BirthDate ?? (object)DBNull.Value)
                , new SqlParameter("@HireDate", employee.HireDate ?? (object)DBNull.Value)
                , new SqlParameter("@Address", employee.Address ?? (object)DBNull.Value)
                , new SqlParameter("@City", employee.City ?? (object)DBNull.Value)
                , new SqlParameter("@Region", employee.Region ?? (object)DBNull.Value)
                , new SqlParameter("@PostalCode", employee.PostalCode ?? (object)DBNull.Value)
                , new SqlParameter("@Country", employee.Country ?? (object)DBNull.Value)
                , new SqlParameter("@HomePhone", employee.HomePhone ?? (object)DBNull.Value)
                , new SqlParameter("@Extension", employee.Extension ?? (object)DBNull.Value)
                , new SqlParameter("@Photo", employee.Photo ?? (object)DBNull.Value)
                , new SqlParameter("@Notes", employee.Notes ?? (object)DBNull.Value)
                , new SqlParameter("@ReportsTo", employee.ReportsTo ?? (object)DBNull.Value)
                , new SqlParameter("@PhotoPath", employee.PhotoPath ?? (object)DBNull.Value)
            };
        }

        public static SqlParameter[] ToParameterArray(this Order order)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@OrderID", order.OrderID)
                , new SqlParameter("@CustomerID", order.CustomerID)
                , new SqlParameter("@EmployeeID", order.EmployeeID ?? (object)DBNull.Value)
                , new SqlParameter("@OrderDate", order.OrderDate)
                , new SqlParameter("@RequiredDate", order.RequiredDate ?? (object)DBNull.Value)
                , new SqlParameter("@ShippedDate", order.ShippedDate ?? (object)DBNull.Value)
                , new SqlParameter("@ShipVia", order.ShipVia ?? (object)DBNull.Value)
                , new SqlParameter("@Freight", order.Freight ?? (object)DBNull.Value)
                , new SqlParameter("@ShipName", order.ShipName ?? (object)DBNull.Value)
                , new SqlParameter("@ShipAddress", order.ShipAddress ?? (object)DBNull.Value)
                , new SqlParameter("@ShipCity", order.ShipCity ?? (object)DBNull.Value)
                , new SqlParameter("@ShipRegion", order.ShipRegion ?? (object)DBNull.Value)
                , new SqlParameter("@ShipPostalCode", order.ShipPostalCode ?? (object)DBNull.Value)
                , new SqlParameter("@ShipCountry", order.ShipCountry ?? (object)DBNull.Value)
            };
        }

        public static SqlParameter[] ToParameterArray(this Order_Detail orderDetail)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@OrderID", orderDetail.OrderID)
                , new SqlParameter("@ProductID", orderDetail.ProductID)
                , new SqlParameter("@UnitPrice", orderDetail.UnitPrice)
                , new SqlParameter("@Quantity", orderDetail.Quantity)
                , new SqlParameter("@Discount", orderDetail.Discount)
            };
        }
    }
}