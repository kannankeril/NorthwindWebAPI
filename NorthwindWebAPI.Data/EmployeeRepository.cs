using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using NorthwindWebAPI.Models;

namespace NorthwindWebAPI.Data
{
    public class EmployeeRepository : SqlDataSource<Employee>
    {
        public override Employee PopulateRecord(IDataReader reader)
        {
            return new Employee(reader);
        }

        public IEnumerable<Employee> Get()
        {
            var sqlCommand = "SELECT EMPLOYEE.EmployeeID, EMPLOYEE.LastName, EMPLOYEE.FirstName, EMPLOYEE.Title, EMPLOYEE.TitleOfCourtesy "
                            + ", EMPLOYEE.BirthDate, EMPLOYEE.HireDate, EMPLOYEE.HomePhone, EMPLOYEE.Extension "
                            + ", EMPLOYEE.[Address], EMPLOYEE.City, EMPLOYEE.Region, EMPLOYEE.PostalCode, EMPLOYEE.Country "
                            + ", EMPLOYEE.Photo, EMPLOYEE.Notes, EMPLOYEE.PhotoPath "
                            + ", EMPLOYEE.ReportsTo, MANAGER.FirstName + ' ' + MANAGER.LastName as Manager "
                            + "FROM dbo.Employees EMPLOYEE "
                            + "   INNER JOIN dbo.Employees MANAGER ON EMPLOYEE.ReportsTo = MANAGER.EmployeeID";

            return base.Get(sqlCommand);
        }

        public IEnumerable<Employee> Get(int employeeId)
        {
            var sqlCommand = "SELECT EMPLOYEE.EmployeeID, EMPLOYEE.LastName, EMPLOYEE.FirstName, EMPLOYEE.Title, EMPLOYEE.TitleOfCourtesy "
                            + ", EMPLOYEE.BirthDate, EMPLOYEE.HireDate, EMPLOYEE.HomePhone, EMPLOYEE.Extension "
                            + ", EMPLOYEE.[Address], EMPLOYEE.City, EMPLOYEE.Region, EMPLOYEE.PostalCode, EMPLOYEE.Country "
                            + ", EMPLOYEE.Photo, EMPLOYEE.Notes, EMPLOYEE.PhotoPath "
                            + ", EMPLOYEE.ReportsTo, MANAGER.FirstName + ' ' + MANAGER.LastName as Manager "
                            + "FROM dbo.Employees EMPLOYEE "
                            + "   INNER JOIN dbo.Employees MANAGER ON EMPLOYEE.ReportsTo = MANAGER.EmployeeID "
                            + "WHERE EMPLOYEE.EmployeeID = @EmployeeId ";
            var parameters = new SqlParameter[] { new SqlParameter("@EmployeeId", employeeId) };

            return base.Get(sqlCommand, parameters);
        }

        public void Add(Employee employee)
        {
            var sqlCommand =
                "IF NOT EXISTS(SELECT 1 FROM dbo.Employees WHERE FirstName = @FirstName AND LastName = @LastName "
                + "											AND Title = @Title AND ReportsTo = @ReportsTo) "
                + " INSERT INTO dbo.Employees "
                + "    (LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, [Address], City "
                + "    , Region, PostalCode, Country, HomePhone, Extension, Photo, Notes, ReportsTo, PhotoPath) "
                + " VALUES(@LastName, @FirstName, @Title, @TitleOfCourtesy, @BirthDate, @HireDate, @Address, @City "
                + "        , @Region, @PostalCode, @Country, @HomePhone, @Extension, @Photo, @Notes, @ReportsTo, @PhotoPath) ";

            base.ExecuteNonQuery(sqlCommand, employee.ToParameterArray());
        }

        public void Update(Employee employee)
        {
            var sqlCommand =
            "UPDATE dbo.Employees "
            + "SET LastName = @LastName, FirstName = @FirstName, Title = @Title, TitleOfCourtesy = @TitleOfCourtesy "
            + "	, BirthDate = @BirthDate, HireDate = @HireDate, [Address]=@Address,City=@City, Region=@Region "
            + "    , PostalCode=@PostalCode, Country=@Country, HomePhone=@HomePhone, Extension=@Extension "
            + "    , Photo=@Photo, Notes=@Notes, ReportsTo=@ReportsTo, PhotoPath=@PhotoPath "
            + "WHERE EmployeeID = @EmployeeID ";

            base.ExecuteNonQuery(sqlCommand, employee.ToParameterArray());
        }

        public void Delete(int employeeId)
        {
            var sqlCommand = "DELETE FROM dbo.Employees WHERE EmployeeID = @EmployeeId";
            var parameters = new SqlParameter[] { new SqlParameter("employeeId", employeeId) };

            base.ExecuteNonQuery(sqlCommand, parameters);
        }
    }
}
