using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using NorthwindWebAPI.Models;

namespace NorthwindWebAPI.Data
{
    public class CustomerRepository : SqlDataSource
    {
        public IEnumerable<Customer> Get()
        {
            var sqlCommand = "SELECT CustomerID, CompanyName, ContactName, ContactTitle "
                            + ", [Address], City, Region, PostalCode, Country, Phone, Fax "
                            + " FROM dbo.Customers";
            List<Customer> customerList = new List<Customer>();
            using (IDataReader reader = ExecuteReader(sqlCommand, null, CommandType.Text))
            {
                while (reader.Read())
                    customerList.Add(new Customer(reader));
            }

            return customerList;
        }

        public Customer Get(string customerId)
        {
            var sqlCommand = "SELECT CustomerID, CompanyName, ContactName, ContactTitle "
                            + ", [Address], City, Region, PostalCode, Country, Phone, Fax "
                            + " FROM dbo.Customers WHERE CustomerID = @CustomerID";
            var parameters = new SqlParameter[] { new SqlParameter("@CustomerID", customerId) };

            Customer customer = null;
            using (IDataReader reader = ExecuteReader(sqlCommand, parameters, CommandType.Text))
            {
                if (reader.Read())
                    customer = new Customer(reader);
            }

            return customer;
        }

        public void Add(Customer customer)
        {
            var sqlCommand = "IF NOT EXISTS(SELECT 1 FROM dbo.Customers WHERE CustomerID = @CustomerID)"
                            + "INSERT INTO dbo.Customers "
                            + " (CustomerID, CompanyName, ContactName, ContactTitle "
                            + ", [Address], City, Region, PostalCode, Country, Phone, Fax) "
                            + "VALUES(@CustomerID, @CompanyName, @ContactName, @ContactTitle "
                            + ", @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax) ";
            ExecuteNonQuery(sqlCommand, customer.ToParameterArray(), CommandType.Text);
        }

        public void Update(Customer customer)
        {
            var sqlCommand = "UPDATE dbo.Customers "
                            + " SET CompanyName = @CompanyName, ContactName = @ContactName "
                            + "     , ContactTitle = @ContactTitle, [Address] = @Address "
                            + "     , City = @City, Region = @Region, PostalCode = @PostalCode "
                            + "     , Country = @Country, Phone = @Phone, Fax = @Fax "
                            + " WHERE CustomerID = @CustomerID ";
            ExecuteNonQuery(sqlCommand, customer.ToParameterArray(), CommandType.Text);
        }

        public void Delete(string customerId)
        {
            var sqlCommand = "DELETE FROM dbo.Customers WHERE CustomerID = @CustomerID ";
            var parameters = new SqlParameter[] { new SqlParameter("@CustomerID", customerId) };
            ExecuteNonQuery(sqlCommand, parameters, CommandType.Text);
        }
    }
}
