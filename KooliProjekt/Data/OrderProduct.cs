namespace KooliProjekt.Data
{
    public class OrderProduct : Entity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public decimal PriceAtOrderTime { get; set; }
    }
}
