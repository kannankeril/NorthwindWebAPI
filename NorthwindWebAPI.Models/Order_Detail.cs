using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace NorthwindWebAPI.Models
{
    [Table("Order Details")]
    public class Order_Detail   //partial
    {
        public Order_Detail()
        { }

        public Order_Detail(IDataReader reader)
        {
            this.OrderID = Convert.ToInt32(reader["OrderID"]);
            this.ProductID = Convert.ToInt32(reader["ProductID"]);
            this.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
            this.Quantity = Convert.ToInt16(reader["Quantity"]);
            this.Discount = Convert.ToInt32(reader["Discount"]);

            this.Product = new Product();
            this.Product.ProductID = this.ProductID;
            this.Product.ProductName = reader["ProductName"].ToString();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
