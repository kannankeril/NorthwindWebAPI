using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using NorthwindWebAPI.Models;

namespace NorthwindWebAPI.Data
{
    public class CustomerRepository : SqlDataSource<Customer>
    {
        public override Customer PopulateRecord(IDataReader reader)
        {
            return new Customer(reader);
        }


        public IEnumerable<Customer> Get()
        {
            var sqlCommand = "SELECT CustomerID, CompanyName, ContactName, ContactTitle "
                            + ", [Address], City, Region, PostalCode, Country, Phone, Fax "
                            + " FROM dbo.Customers";

            return base.Get(sqlCommand);
        }

        public IEnumerable<Customer> Get(string customerId)
        {
            var sqlCommand = "SELECT CustomerID, CompanyName, ContactName, ContactTitle "
                            + ", [Address], City, Region, PostalCode, Country, Phone, Fax "
                            + " FROM dbo.Customers WHERE CustomerID = @CustomerID";
            var parameters = new SqlParameter[] { new SqlParameter("@CustomerID", customerId) };

            return base.Get(sqlCommand, parameters);
        }

        public void Add(Customer customer)
        {
            var sqlCommand = "IF NOT EXISTS(SELECT 1 FROM dbo.Customers WHERE CustomerID = @CustomerID)"
                            + "INSERT INTO dbo.Customers "
                            + " (CustomerID, CompanyName, ContactName, ContactTitle "
                            + ", [Address], City, Region, PostalCode, Country, Phone, Fax) "
                            + "VALUES(@CustomerID, @CompanyName, @ContactName, @ContactTitle "
                            + ", @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax) ";

            base.ExecuteNonQuery(sqlCommand, customer.ToParameterArray());
        }

        public void Update(Customer customer)
        {
            var sqlCommand = "UPDATE dbo.Customers "
                            + " SET CompanyName = @CompanyName, ContactName = @ContactName "
                            + "     , ContactTitle = @ContactTitle, [Address] = @Address "
                            + "     , City = @City, Region = @Region, PostalCode = @PostalCode "
                            + "     , Country = @Country, Phone = @Phone, Fax = @Fax "
                            + " WHERE CustomerID = @CustomerID ";

            base.ExecuteNonQuery(sqlCommand, customer.ToParameterArray());
        }

        public void Delete(string customerId)
        {
            var sqlCommand = "DELETE FROM dbo.Customers WHERE CustomerID = @CustomerID ";
            var parameters = new SqlParameter[] { new SqlParameter("@CustomerID", customerId) };

            base.ExecuteNonQuery(sqlCommand, parameters);
        }
    }
}
