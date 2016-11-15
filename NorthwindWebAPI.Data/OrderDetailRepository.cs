using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using NorthwindWebAPI.Models;

namespace NorthwindWebAPI.Data
{
    public class OrderDetailRepository : SqlDataSource
    {
        #region GET methods
        public IEnumerable<Order_Detail> GET()
        {
            throw new NotImplementedException("This web service does not allow inserting items outside of an order");
        }

        /// <summary>
        /// Get order details for a given order
        /// </summary>
        /// <param name="orderId">the order for which details have been requested</param>
        /// <returns></returns>
        public IEnumerable<Order_Detail> Get(int orderId)
        {
            var sqlCommand =
                "SELECT OrderID, DETAILS.ProductID, DETAILS.UnitPrice, Quantity, Discount, PRODUCT.ProductName "
                + "FROM dbo.[Order Details] DETAILS "
                + "	INNER JOIN dbo.Products PRODUCT ON DETAILS.ProductID = PRODUCT.ProductID "
                + "WHERE OrderID = @OrderID ";
            var parameters = new SqlParameter[] { new SqlParameter("@OrderID", orderId) };
            List<Order_Detail> orderDetailList = new List<Order_Detail>();

            using (IDataReader reader = ExecuteReader(sqlCommand, parameters, CommandType.Text))
            {
                while (reader.Read())
                    orderDetailList.Add(new Order_Detail(reader));
            }

            return orderDetailList;
        }
        #endregion


        #region ADD methods
        public void Add(IEnumerable<Order_Detail> orderItems, SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            foreach(Order_Detail item in orderItems)
                Add(item, sqlConnection, sqlTransaction);
        }

        public void Add(Order_Detail orderItem, SqlConnection sqlConnection = null, SqlTransaction sqlTransaction = null)
        {
            var sqlCommand =
                "IF NOT EXISTS(SELECT 1 FROM dbo.[Order Details] WHERE OrderID = @OrderID AND ProductID = @ProductID) "
                + "INSERT INTO dbo.[Order Details] "
                + "    (OrderID, ProductID, UnitPrice, Quantity, Discount) "
                + "VALUES(@OrderID, @ProductID, @UnitPrice, @Quantity, @Discount) ";

            ExecuteNonQuery(sqlCommand, orderItem.ToParameterArray(), CommandType.Text, sqlConnection, sqlTransaction);
        }
        #endregion


        #region UPDATE methods
        public void Update(IEnumerable<Order_Detail> orderItems, SqlConnection sqlConnection, SqlTransaction sqlTransaction)
        {
            foreach(var item in orderItems)
                Update(item, sqlConnection, sqlTransaction);
        }

        public void Update(Order_Detail orderDetail, SqlConnection sqlConnection = null, SqlTransaction sqlTransaction = null)
        {
            var sqlCommand =
                "UPDATE dbo.[Order Details] "
                + "   SET UnitPrice = @UnitPrice, Quantity = @Quantity, Discount = @Discount "
                + "WHERE OrderID = @OrderID AND ProductID = @ProductID ";

            ExecuteNonQuery(sqlCommand, orderDetail.ToParameterArray(), CommandType.Text, sqlConnection, sqlTransaction);
        }
        #endregion


        #region DELETE methods
        public void Delete(int orderId, SqlConnection sqlConnection = null, SqlTransaction sqlTransaction = null)
        {
            var sqlCommand = "DELETE FROM dbo.[Order Details] WHERE OrderID = @OrderID ";
            var parameters = new SqlParameter[] { new SqlParameter("@OrderID", orderId) };
            ExecuteNonQuery(sqlCommand, parameters, CommandType.Text, sqlConnection, sqlTransaction);
        }

        public void Delete(int orderId, int productId, SqlConnection sqlConnection = null, SqlTransaction sqlTransaction = null)
        {
            var sqlCommand = "DELETE FROM dbo.[Order Details] "
                            + "WHERE OrderID = @OrderID AND ProductID = @ProductID ";
            var parameters = new SqlParameter[] { new SqlParameter("@OrderID", orderId)
                                                , new SqlParameter("@ProductID", productId) };
            ExecuteNonQuery(sqlCommand, parameters, CommandType.Text, sqlConnection, sqlTransaction);
        }
        #endregion
    }
}
