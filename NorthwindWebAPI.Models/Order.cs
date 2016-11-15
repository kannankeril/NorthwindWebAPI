using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace NorthwindWebAPI.Models
{
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Order_Details = new HashSet<Order_Detail>();
        }

        public Order(IDataReader reader)
        {
            this.OrderID = Convert.ToInt32(reader["OrderID"]);
            this.CustomerID = reader["CustomerID"] as string;
            this.EmployeeID = reader["EmployeeID"] as int?;
            this.OrderDate = reader["OrderDate"] as DateTime?;
            this.RequiredDate = reader["RequiredDate"] as DateTime?;
            this.ShippedDate = reader["ShippedDate"] as DateTime?;
            this.ShipVia = reader["ShipVia"] as int?;
            this.Freight = reader["Freight"] as decimal?;
            this.ShipName = reader["ShipName"] as string;
            this.ShipAddress = reader["ShipAddress"] as string;
            this.ShipCity = reader["ShipCity"] as string;
            this.ShipRegion = reader["ShipRegion"] as string;
            this.ShipPostalCode = reader["ShipPostalCode"] as string;
            this.ShipCountry = reader["ShipCountry"] as string;

            this.Customer = new Customer();
            this.Customer.CustomerID = this.CustomerID;
            this.Customer.CompanyName = reader["CompanyName"] as string;
            this.Customer.ContactName = reader["ContactName"] as string;

            this.Employee = new Employee();
            this.Employee.EmployeeID = this.EmployeeID ?? 0;
            this.Employee.FirstName = reader["FirstName"] as string;
            this.Employee.LastName = reader["LastName"] as string;

            Order_Details = new HashSet<Order_Detail>();
        }

        public int OrderID { get; set; }

        [StringLength(5)]
        public string CustomerID { get; set; }

        public int? EmployeeID { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int? ShipVia { get; set; }

        [Column(TypeName = "money")]
        public decimal? Freight { get; set; }

        [StringLength(40)]
        public string ShipName { get; set; }

        [StringLength(60)]
        public string ShipAddress { get; set; }

        [StringLength(15)]
        public string ShipCity { get; set; }

        [StringLength(15)]
        public string ShipRegion { get; set; }

        [StringLength(10)]
        public string ShipPostalCode { get; set; }

        [StringLength(15)]
        public string ShipCountry { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Details { get; set; }

        public virtual Shipper Shipper { get; set; }
    }
}
