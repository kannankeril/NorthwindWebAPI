namespace NorthwindWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Employees1 = new HashSet<Employee>();
            Orders = new HashSet<Order>();
            Territories = new HashSet<Territory>();
        }

        public Employee(IDataReader reader)
        {
            this.EmployeeID = Convert.ToInt32(reader["EmployeeID"]);
            this.TitleOfCourtesy = reader["TitleOfCourtesy"] as string;
            this.FirstName = reader["FirstName"].ToString();
            this.LastName = reader["LastName"].ToString();
            this.Title = reader["Title"] as string;
            this.BirthDate = reader["BirthDate"] as DateTime?;
            this.HireDate = reader["HireDate"] as DateTime?;
            this.Address = reader["Address"] as string;
            this.City = reader["City"] as string;
            this.Region = reader["Region"] as string;
            this.PostalCode = reader["PostalCode"] as string;
            this.Country = reader["Country"] as string;
            this.HomePhone = reader["HomePhone"] as string;
            this.Extension = reader["Extension"] as string;
            this.Photo = reader["Photo"] as byte[];
            this.Notes = reader["Notes"] as string;
            this.ReportsTo = reader["ReportsTo"] as int?;
            this.PhotoPath = reader["PhotoPath"] as string;
            //this.Manager = reader["Manager"].ToString();
        }

        public int EmployeeID { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        [StringLength(25)]
        public string TitleOfCourtesy { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        [StringLength(60)]
        public string Address { get; set; }

        [StringLength(15)]
        public string City { get; set; }

        [StringLength(15)]
        public string Region { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(15)]
        public string Country { get; set; }

        [StringLength(24)]
        public string HomePhone { get; set; }

        [StringLength(4)]
        public string Extension { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        public int? ReportsTo { get; set; }

        [StringLength(255)]
        public string PhotoPath { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees1 { get; set; }

        public virtual Employee Employee1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Territory> Territories { get; set; }
    }
}
