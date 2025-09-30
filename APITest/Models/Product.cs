using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APITest.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [StringLength(100)]
        public required string ProductName { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public required decimal Price { get; set; }

        public bool IsAvailable { get; set; }

        public DateTime DateAdded { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Product()
        {
            Orders = new HashSet<Order>();
        }
    }
}