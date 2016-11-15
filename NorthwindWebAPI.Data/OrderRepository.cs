using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using NorthwindWebAPI.Models;

namespace NorthwindWebAPI.Data
{
    public class OrderRepository : SqlDataSource<Order>
    {
        private OrderDetailRepository _orderDetailRepository;
        public OrderRepository()
        {
            _orderDetailRepository = new OrderDetailRepository();
        }

        public override Order PopulateRecord(IDataReader reader)
        {
            return new Order(reader);
        }


        public IEnumerable<Order> Get()
        {
            var sqlCommand =
                "SELECT OrderID, ORDERS.CustomerID, ORDERS.EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia "
                + "	, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry "
                + "	, CUSTOMERS.CompanyName, CUSTOMERS.ContactName, EMPLOYEES.FirstName, EMPLOYEES.LastName "
                + "FROM dbo.Orders ORDERS "
                + " INNER JOIN dbo.Customers CUSTOMERS ON ORDERS.CustomerID = CUSTOMERS.CustomerID "
                + " INNER JOIN dbo.Employees EMPLOYEES ON ORDERS.EmployeeID = EMPLOYEES.EmployeeID ";

            return base.Get(sqlCommand);
        }

        public IEnumerable<Order> Get(int OrderId)
        {
            var sqlCommand =
                "SELECT OrderID, ORDERS.CustomerID, ORDERS.EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia "
                + "	, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry "
                + "	, CUSTOMERS.CompanyName, CUSTOMERS.ContactName, EMPLOYEES.FirstName, EMPLOYEES.LastName "
                + "FROM dbo.Orders ORDERS "
                + " INNER JOIN dbo.Customers CUSTOMERS ON ORDERS.CustomerID = CUSTOMERS.CustomerID "
                + " INNER JOIN dbo.Employees EMPLOYEES ON ORDERS.EmployeeID = EMPLOYEES.EmployeeID "
                + "WHERE OrderID = @OrderID ";
            var parameters = new SqlParameter[] { new SqlParameter("@OrderID", OrderId) };

            return base.Get(sqlCommand, parameters);
        }

        public void Add(Order order)
        {
            var sqlCommand =
                    "INSERT INTO dbo.Orders "
                    + "    (CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight "
                    + "    , ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry) "
                    + " VALUES "
                    + "   (@CustomerID, @EmployeeID, @OrderDate, @RequiredDate, @ShippedDate, @ShipVia, @Freight "
                    + "    , @ShipName, @ShipAddress, @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry); "
                    
                    + "SELECT SCOPE_IDENTITY() ";
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            try
            {
                var orderId = base.ExecuteScalar(sqlCommand, order.ToParameterArray()
                                            , CommandType.Text, sqlConnection, sqlTransaction);
                if (orderId == DBNull.Value)
                    throw new NullReferenceException("Order creation failed");

                order.OrderID = Convert.ToInt32(orderId);
                foreach (var item in order.Order_Details)
                    item.OrderID = order.OrderID;

                _orderDetailRepository.Add(order.Order_Details, sqlConnection, sqlTransaction);

            }
            catch (Exception)
            {
                sqlTransaction.Rollback();
                sqlConnection.Close();
                throw;
            }

            sqlTransaction.Commit();
            sqlConnection.Close();
        }

        public void Update(Order order)
        {
            var sqlCommand =
                "UPDATE dbo.Orders "
                + "   SET CustomerID = @CustomerID, EmployeeID = @EmployeeID, OrderDate = @OrderDate "
                + "      ,RequiredDate = @RequiredDate, ShippedDate = @ShippedDate, ShipVia = @ShipVia "
                + "      ,Freight = @Freight, ShipName = @ShipName, ShipAddress = @ShipAddress, ShipCity = @ShipCity "
                + "	  , ShipRegion = @ShipRegion, ShipPostalCode = @ShipPostalCode, ShipCountry = @ShipCountry "
                + "WHERE OrderID = @OrderID ";

            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            try
            {
                base.ExecuteScalar(sqlCommand, order.ToParameterArray()
                                            , CommandType.Text, sqlConnection, sqlTransaction);

                _orderDetailRepository.Update(order.Order_Details, sqlConnection, sqlTransaction);

            }
            catch (Exception)
            {
                sqlTransaction.Rollback();
                sqlConnection.Close();
                throw;
            }

            sqlTransaction.Commit();
            sqlConnection.Close();

        }

        public void Delete(int orderId)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            try
            {
                _orderDetailRepository.Delete(orderId, sqlConnection, sqlTransaction);

                var sqlCommand = "DELETE FROM dbo.Orders WHERE OrderID = @OrderID  ";
                var parameters = new SqlParameter[] { new SqlParameter("@OrderID", orderId) };
                ExecuteNonQuery(sqlCommand, parameters, CommandType.Text, sqlConnection, sqlTransaction);
            }
            catch (Exception)
            {
                sqlTransaction.Rollback();
                sqlConnection.Close();
                throw;
            }

            sqlTransaction.Commit();
            sqlConnection.Close();
        }
    }
}
