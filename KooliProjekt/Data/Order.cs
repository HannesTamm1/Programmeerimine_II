using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        [Required]
        public string Status { get; set; }

        // Foreign key to User
        [Required]
        public string UserId { get; set; }

        // Navigation property to User
        [Required]
        public User User { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; }
        public string Title { get; set; }

        public Order()
        {
            OrderProducts = new List<OrderProduct>();
        }

        public static implicit operator Order(Order v)
        {
            throw new NotImplementedException();
        }
    }
}
