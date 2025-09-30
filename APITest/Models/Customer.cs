using System.ComponentModel.DataAnnotations;

namespace APITest.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [StringLength(50)]
        public required string FirstName { get; set; }

        [StringLength(50)]
        public required string LastName { get; set; }

        [StringLength(100)]
        public required string Email { get; set; }

        public bool IsActive { get; set; }

        public DateTime RegisteredDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new HashSet<Order>();
        }
    }
}