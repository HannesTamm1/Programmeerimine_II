using System.Numerics;

namespace KooliProjekt.Data
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime LineItem { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
    }
}
