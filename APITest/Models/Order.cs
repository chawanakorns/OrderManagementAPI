using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITest.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }


        // Foreign Keys เพื่อเชื่อมกันระหว่างสอง Tables
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        // -----------------------------------


        public required DateTime OrderDate { get; set; }

        public int Quantity { get; set; }

        public bool IsShipped { get; set; }



        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}