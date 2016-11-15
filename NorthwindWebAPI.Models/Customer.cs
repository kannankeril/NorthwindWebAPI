using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace NorthwindWebAPI.Models
{
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public Customer(IDataReader reader)
        {
            this.CustomerID = reader["CustomerID"].ToString();
            this.CompanyName = reader["CompanyName"].ToString();
            this.ContactName = reader["ContactName"].ToString();
            this.ContactTitle = reader["ContactTitle"].ToString();
            this.Address = reader["Address"].ToString();
            this.City = reader["City"].ToString();
            this.Region = reader["Region"] == DBNull.Value ? string.Empty : reader["Region"].ToString();
            this.PostalCode = reader["PostalCode"].ToString();
            this.Country = reader["Country"].ToString();
            this.Phone = reader["Phone"].ToString();
            this.Fax = reader["Fax"] == DBNull.Value ? string.Empty : reader["Fax"].ToString();

            ///ToDo: create constructor that also populates customer orders
            Orders = new HashSet<Order>();
        }

        /// <summary>
        /// This property/column is a soundex value and needs to be input by a user
        /// </summary>
        [Required]
        [StringLength(5)]
        public string CustomerID { get; set; }

        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(30)]
        public string ContactName { get; set; }

        [Required]
        [StringLength(30)]
        public string ContactTitle { get; set; }

        [Required]
        [StringLength(60)]
        public string Address { get; set; }

        [Required]
        [StringLength(15)]
        public string City { get; set; }

        [StringLength(15)]
        public string Region { get; set; }

        [Required]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(15)]
        public string Country { get; set; }

        [Required]
        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
