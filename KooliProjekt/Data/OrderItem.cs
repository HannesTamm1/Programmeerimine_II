namespace KooliProjekt.Data
{
    public class OrderItem
    {
            public int OrderItemId { get; set; }
            public int OrderId { get; set; }
            public int ProductId { get; set; }
            public float PriceAtOrder { get; set; }
            public int Quantity { get; set; }
    }
}
